using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Attributes;
using Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Directives;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Imports;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Modules;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Helpers;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Errors;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Events;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Header;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Imports;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Modifiers;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Parameters;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.State;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Structs;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Version;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Synthesizer
{
    public class SoliditySynthesizer : ILanguageSpecificModelSynthesizer<SolidityLanguageMetamodel>
    {
        private readonly VersionModel _defaultVersion = new VersionModel() { Minimum = new() { Major=0, Minor=8, Revision = 20 } };

        public SolidityLanguageMetamodel Synthesize(ContextMetamodel file)
        {
            var fileHeader = GenerateFileHeader(file);
            var contracts = GenerateContracts(file);
            var result = new SolidityLanguageMetamodel() { FileHeader = fileHeader, Contracts = contracts };
            return result;
        }

        #region File Header

        private FileHeaderModel GenerateFileHeader(ModuleFileDefinition file) 
        {
            var versionDirective = file.Directives.Where(x => x.Kind == DirectiveKind.Version);
            var licenseDirective = file.Directives.FirstOrDefault(x => x.Kind == DirectiveKind.License);
            var version = GenerateVersion(versionDirective);
            var license = GenerateLicense(licenseDirective);
            var result = new FileHeaderModel() { License = license, Version = version ?? _defaultVersion };
            return result;
        }

        private VersionModel GenerateVersion(IEnumerable<DirectiveDefinition> versionDirectives)
        {
            if(!versionDirectives.Any()) return _defaultVersion;
            var version1 = ParseVersion(versionDirectives.First().Value);
            var version2 = versionDirectives.Skip(1).Any() ? ParseVersion(versionDirectives.Skip(1).First().Value) : null;
            if(version1 == version2) return new VersionModel() { Maximum = version1, Minimum = version2 };
            if(version1 > version2) return new VersionModel() { Maximum = version1, Minimum = version2 };
            return new VersionModel() { Maximum = version2, Minimum = version1 };
        }

        private SpdxLicense GenerateLicense(DirectiveDefinition? licenseDirective)
        {
            if (licenseDirective != null)
            {
                var raw = licenseDirective.Value?.Trim()
                                  ?? throw new FormatException("License value is null or empty.");

                var enumType = typeof(SpdxLicense);
                foreach (var field in enumType.GetFields(BindingFlags.Public | BindingFlags.Static))
                {
                    var label = field.GetCustomAttribute<EnumLabelAttribute>();
                    if (label != null && string.Equals(label.Value, raw, StringComparison.OrdinalIgnoreCase))
                        return (SpdxLicense)field.GetValue(null)!;
                }

                throw new InvalidOperationException($"Unsupported SPDX license identifier '{raw}'.");
            }

            return SpdxLicense.MIT;
        }


        private SoftwareVersion ParseVersion(string versionString)
        {
            if (string.IsNullOrWhiteSpace(versionString))
                throw new FormatException("Version string is null or empty.");

            var match = Regex.Match(versionString, "\\d+(\\.\\d+)*");
            if (!match.Success)
                throw new FormatException($"Invalid version format: '{versionString}'.");

            var versionTokens = ParseVersionTokens(versionString);
            
            return  new SoftwareVersion() { Major = versionTokens[0], Minor = versionTokens[1], Revision = versionTokens[2] };
        }

        public static int[] ParseVersionTokens(string versionString)
        {
            if (string.IsNullOrWhiteSpace(versionString))
                throw new ArgumentException("A versão não pode ser nula ou vazia.", nameof(versionString));

            var parts = versionString
                .Split(['.'], StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
                throw new ArgumentException("A versão deve conter pelo menos um segmento numérico.", nameof(versionString));

            var tokens = parts.Select(p =>
            {
                if (!int.TryParse(p, out var n))
                    throw new FormatException($"Segmento de versão inválido: '{p}'. Esperava-se um número inteiro.");
                return n;
            })
            .ToArray();

            return tokens;
        }

        #endregion

        #region Contract
        private SolidityContractModel[] GenerateContracts(ModuleFileDefinition file)
        {
            var result = new List<SolidityContractModel>();
            foreach (var module in file.Modules)
            {
                var contract = GenerateContract(module, file.Imports);
                result.Add(contract);
            }
            return result.ToArray();
        }

        private SolidityContractModel GenerateContract(ModuleDefinition module, IEnumerable<ImportDefinition> importDefinitions)
        {
            var interfaces = GenerateInterfaces(module);
            var imports = GenerateImports(module, importDefinitions);
            var baseContracts = GenerateBaseContracts(module, importDefinitions);
            var stateProperties = GenerateStateProperties(module, importDefinitions);
            var enums = GenerateEnums(module, importDefinitions);
            var structs = GenerateStructs(module, importDefinitions);
            var modifiers = GenerateModifiers(module);
            var errors = GenerateErrors(module);
            var events = GenerateEvents(module);
            var functions = GenerateFunctions(module);
            var constructorParameters = GenerateConstructorParameters(module);

            var result = new SolidityContractModel()
            {
                Name = module.Name,
                Interfaces = interfaces,
                Imports = imports,
                BaseContracts = baseContracts,
                StateProperties = stateProperties,
                Enums = enums,
                Structs = structs,
                Modifiers = modifiers,
                Errors = errors,
                Events = events,
                Functions = functions,
                ConstructorParameters = constructorParameters,
            };
            return result;
        }

        private List<ConstructorParameterModel> GenerateConstructorParameters(ModuleDefinition module)
        {
            var result = new List<ConstructorParameterModel>();
            int paramIdx = 0;
            foreach (var function in module.Functions)
            {
                if (function.Kind != FunctionKind.Constructor)
                    continue;

                foreach (var parameter in function.Parameters) 
                {
                    string? assignedTo = null;

                    // Not sure if this is the right way to handle this, but it seems like
                    if (!string.IsNullOrEmpty(parameter.Name))
                    {
                        var matchedField = module.Fields
                            .FirstOrDefault(field => field.Value == parameter.Name);

                        if (matchedField != null)
                        {
                            assignedTo = matchedField.Name;
                        }
                    }

                    var constructorParam = new ConstructorParameterModel
                    {
                        Type =ContextTypeReferenceSyntaxHelper.MapToSolidityTypeReference(parameter.Type),
                        Name = parameter.Name,
                        Index = paramIdx,
                        Value = parameter.Value,
                        Location = SolidityMemoryLocation.None, 
                        AssignedTo = assignedTo
                    };
                    paramIdx++;
                    result.Add(constructorParam);
                }
            }

            return result;
        }

        private List<BaseFunctionModel> GenerateFunctions(ModuleDefinition module)
        {
            var result = new List<BaseFunctionModel>();
            foreach (var function in module.Functions)
            {
                var parameters = function.Parameters.Select((p, index) => new FunctionParameterModel
                {
                    Name = p.Name,
                    Type = ContextTypeReferenceSyntaxHelper.MapToSolidityTypeReference(p.Type),
                    Index = index, 
                    Value = p.Value,
                    Location = SolidityMemoryLocation.None
                }).ToList();

                var returnParameters = function.ReturnParameters.Select((p, index) => new ReturnParameterModel
                {
                    Name = p.Name,
                    Type = ContextTypeReferenceSyntaxHelper.MapToSolidityTypeReference(p.Type),
                    Index = index,
                    Value = p.Value,
                    Location = SolidityMemoryLocation.None
                }).ToList();

                var statements = function.Body.Select(s => StatementSoliditySyntaxHelper.MapStatement(s))
                    .ToList();
                var modifiers = new List<ModifierModel>();

                    foreach (var modifierDefinition in function.Modifiers)
                    {
                        var modifierModel = CreateModifierModel(modifierDefinition);
                        modifiers.Add(modifierModel);
                    }
                

                BaseFunctionModel funcModel = function.Kind switch
                {
                    FunctionKind.Fallback => new FallbackFunctionModel(),
                    FunctionKind.Receive => new ReceiveFunctionModel(),
                    FunctionKind.Normal => new NormalFunctionModel() { 
                        Name = function.Name, 
                        Parameters = parameters, 
                        Visibility = ContextTypeReferenceSyntaxHelper.MapToSolidityVisibility(function.Visibility),
                        ReturnParameters = returnParameters,
                        Statements = statements,
                        Modifiers = modifiers,
                    },
                    FunctionKind.Constructor => throw new NotImplementedException(), // Needs to be implemented ?
                    _ => throw new Exception($"Unsupported function kind: {function.Kind}"),
                };
                result.Add(funcModel);
            }
            return result;
        }


        private List<EventModel> GenerateEvents(ModuleDefinition module)
        {
            var result = new List<EventModel>();
            foreach (var trigger in module.Triggers)
            {
                if (trigger.Kind == TriggerKind.Log)
                {
                    var paramIdx = 0;
                    var eventModel = new EventModel()
                    {
                        Name = trigger.Name,
                        Parameters = trigger.Parameters.Select(p => new EventParameterModel
                        {
                            Name = p.Name,
                            Type = ContextTypeReferenceSyntaxHelper.MapToSolidityTypeReference(p.Type),
                            Index = paramIdx,
                            Value = p.Value,
                        }).ToList(),
                    };
                    result.Add(eventModel); 
                }
            }
            return result;
        }

        private List<ErrorModel> GenerateErrors(ModuleDefinition module)
        {

            var result = new List<ErrorModel>();

            foreach (var trigger in module.Triggers)
            {
                if (trigger.Kind == TriggerKind.Error)
                {
                    var errorModel = new ErrorModel()
                    {
                        Name = trigger.Name,
                        Parameters = trigger.Parameters?.Select((p, index) => new ErrorParameterModel
                        {
                            Name = p.Name,
                            Type = ContextTypeReferenceSyntaxHelper.MapToSolidityTypeReference(p.Type),
                            Index = index, 
                            Value = p.Value
                        }).ToList() ?? new List<ErrorParameterModel>()
                    };
                    result.Add(errorModel);
                }
            }

            return result;
        }

        private List<ModifierModel> GenerateModifiers(ModuleDefinition module)
        {
            var result = new List<ModifierModel>();

            if (module.Modifiers != null && module.Modifiers.Any())
            {
                foreach (var modifierDefinition in module.Modifiers)
                {
                    var modifierModel = CreateModifierModel(modifierDefinition);
                    result.Add(modifierModel);
                }
            }

            return result;
        }

        private ModifierModel CreateModifierModel(ModifierDefinition modifierDefinition)
        {
            if (string.IsNullOrWhiteSpace(modifierDefinition.Name))
            {
                throw new ArgumentException("Modifier definition must have a valid name", nameof(modifierDefinition));
            }

            var parameters = GenerateModifierParameters(modifierDefinition.Arguments);
            var arguments = ExtractArgumentNames(modifierDefinition.Arguments);

            var modifierModel = new ModifierModel()
            {
                Name = modifierDefinition.Name,
                Parameters = parameters,
                Arguments = arguments,
                Body = GenerateModifierBody(modifierDefinition)
            };

            return modifierModel;
        }

        private List<ModifierParameterModel> GenerateModifierParameters(List<ParameterDefinition> parameterDefinitions)
        {
            var result = new List<ModifierParameterModel>();

            if (parameterDefinitions != null && parameterDefinitions.Any())
            {
                for (int i = 0; i < parameterDefinitions.Count; i++)
                {
                    var paramDef = parameterDefinitions[i];

                    if (string.IsNullOrWhiteSpace(paramDef.Name))
                    {
                        throw new ArgumentException($"Parameter at index {i} must have a valid name");
                    }

                    if (paramDef.Type == null)
                    {
                        throw new ArgumentException($"Parameter '{paramDef.Name}' must have a valid type");
                    }

                    try
                    {
                        var modifierParameter = new ModifierParameterModel
                        {
                            Name = paramDef.Name,
                            Type = ContextTypeReferenceSyntaxHelper.MapToSolidityTypeReference(paramDef.Type),
                            Index = i,
                            Value = paramDef.Value,
                            Location = DetermineMemoryLocation(paramDef.Type)
                        };

                        result.Add(modifierParameter);
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException($"Failed to map parameter '{paramDef.Name}' in modifier: {ex.Message}", ex);
                    }
                }
            }

            return result;
        }

        private List<string> ExtractArgumentNames(List<ParameterDefinition> parameterDefinitions)
        {
            var result = new List<string>();

            if (parameterDefinitions != null && parameterDefinitions.Any())
            {
                foreach (var paramDef in parameterDefinitions)
                {
                    if (!string.IsNullOrWhiteSpace(paramDef.Name))
                    {
                        result.Add(paramDef.Name);
                    }
                }
            }

            return result;
        }

        private SolidityMemoryLocation DetermineMemoryLocation(TypeReference typeReference)
        {
            return typeReference.Kind switch
            {
                TypeReferenceKind.Simple => SolidityMemoryLocation.None,
                TypeReferenceKind.Array => SolidityMemoryLocation.Memory,
                TypeReferenceKind.Mapping => SolidityMemoryLocation.Storage,
                TypeReferenceKind.Custom => DetermineCustomTypeLocation(typeReference),
                TypeReferenceKind.Tuple => SolidityMemoryLocation.Memory,
                _ => SolidityMemoryLocation.None
            };
        }

        private SolidityMemoryLocation DetermineCustomTypeLocation(TypeReference typeReference)
        {
            if (!string.IsNullOrEmpty(typeReference.TypeName))
            {
                return SolidityMemoryLocation.Memory;
            }

            return SolidityMemoryLocation.None;
        }

        private string GenerateModifierBody(ModifierDefinition modifierDefinition)
        {
            return "_;"; //Not sure how to generate the body yet, placeholder for now
        }

        private List<ModifierModel> GenerateModifiersFromFunctions(ModuleDefinition module)
        {
            var result = new List<ModifierModel>();
            var distinctModifiers = new HashSet<string>();

            foreach (var function in module.Functions)
            {
                if (function.Modifiers != null && function.Modifiers.Any())
                {
                    foreach (var modifierUsage in function.Modifiers)
                    {
                        if (!string.IsNullOrWhiteSpace(modifierUsage.Name) &&
                            distinctModifiers.Add(modifierUsage.Name))
                        {
                            var modifierModel = new ModifierModel()
                            {
                                Name = modifierUsage.Name,
                                Parameters = GenerateModifierParameters(modifierUsage.Arguments),
                                Arguments = ExtractArgumentNames(modifierUsage.Arguments),
                                Body = $"// Modifier {modifierUsage.Name} implementation\n_;"
                            };

                            result.Add(modifierModel);
                        }
                    }
                }
            }

            return result;
        }

        private List<StructModel> GenerateStructs(ModuleDefinition module, IEnumerable<ImportDefinition> importDefinitions)
        {
            var result = new List<StructModel>();

            if (module.Structs != null && module.Structs.Any())
            {
                foreach (var structDefinition in module.Structs)
                {
                    var properties = GenerateStructProperties(structDefinition.Fields);

                    var structModel = new StructModel()
                    {
                        Name = structDefinition.Name,
                        Properties = properties.ToArray()
                    };

                    result.Add(structModel);
                }
            }

            return result;
        }

        private List<StructPropertyModel> GenerateStructProperties(List<FieldDefinition> fields)
        {
            var result = new List<StructPropertyModel>();

            if (fields != null && fields.Any())
            {
                foreach (var field in fields)
                {
                    var property = new StructPropertyModel()
                    {
                        Name = field.Name,
                        DataType = ContextTypeReferenceSyntaxHelper.MapToSolidityTypeReference(field.Type)
                    };

                    result.Add(property);
                }
            }

            return result;
        }

        private List<EnumModel> GenerateEnums(ModuleDefinition module, IEnumerable<ImportDefinition> importDefinitions)
        {
            var result = new List<EnumModel>();

            if (module.Enums != null && module.Enums.Any())
            {
                foreach (var enumDefinition in module.Enums)
                {
                    var enumModel = new EnumModel()
                    {
                        Name = enumDefinition.Name,
                        Values = enumDefinition.Members.ToList()
                    };

                    result.Add(enumModel);
                }
            }

            return result;
        }

        private List<StatePropertyModel> GenerateStateProperties(ModuleDefinition module, IEnumerable<ImportDefinition> importDefinitions)
        {
            var result = new List<StatePropertyModel>();
            foreach (FieldDefinition fieldDefinition in module.Fields)
            {
                var stateVariable = new StatePropertyModel()
                {
                    Name = fieldDefinition.Name,
                    Type = ContextTypeReferenceSyntaxHelper.MapToSolidityTypeReference(fieldDefinition.Type),
                    Visibility = ContextTypeReferenceSyntaxHelper.MapToSolidityVisibility(fieldDefinition.Visibility),
                    IsImmutable = fieldDefinition.IsImmutable,
                    InitialValue = fieldDefinition.Value
                };
                result.Add(stateVariable);
            }
            return result;
        }

        private List<AbstractionImportModel> GenerateBaseContracts(ModuleDefinition module, IEnumerable<ImportDefinition> importDefinitions)
        {
            var result = new List<AbstractionImportModel>();

            foreach (var import in importDefinitions)
            {
                if (string.IsNullOrWhiteSpace(import.Name))
                    continue; 

                var abstraction = new AbstractionImportModel
                {
                    Name = import.Name!,
                    PathName = import.Path,
                    Alias = import.Alias,
                    Code = null,
                    ConstructorParameters = GenerateConstructorParameters(module)
                };

                result.Add(abstraction);
            }

            return result;
        }


        private List<ImportModel> GenerateImports(ModuleDefinition module, IEnumerable<ImportDefinition> importDefinitions)
        {
            var result = new List<ImportModel>();

            foreach (var import in importDefinitions)
            {
                var importModel = new ImportModel
                {
                    PathName = import.Path,
                    Alias = import.Alias,
                    Code = null 
                };

                result.Add(importModel);
            }

            return result;
        }

        // TODO Incomplete
        private List<InterfaceImportModel> GenerateInterfaces(ModuleDefinition module)
        {
            var result = new List<InterfaceImportModel>();
            foreach (var interfaceDefinition in module.Implements)
            {
                var interfaceImport = new InterfaceImportModel()
                {
                    Name = interfaceDefinition.Name,
                    
                };
                result.Add(interfaceImport);
            }
            return result;
        }
        #endregion

    }
}
