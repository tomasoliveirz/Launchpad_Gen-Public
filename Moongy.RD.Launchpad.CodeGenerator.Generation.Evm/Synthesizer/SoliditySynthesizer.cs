using System.Reflection;
using System.Text.RegularExpressions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Attributes;
using Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Directives;
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

            if (module.Fields != null && module.Fields.Any())
            {
                foreach (var field in module.Fields)
                {
                    var parameter = new ConstructorParameterModel()
                    {
                        AssignedTo = 
                    };
                    result.Add(parameter);
                }
            }

            return result;
        }

        private List<BaseFunctionModel> GenerateFunctions(ModuleDefinition module)
        {
            throw new NotImplementedException();
        }

        private List<EventModel> GenerateEvents(ModuleDefinition module)
        {
            throw new NotImplementedException();
        }

        private List<ErrorModel> GenerateErrors(ModuleDefinition module)
        {
            throw new NotImplementedException();
        }

        private List<ModifierModel> GenerateModifiers(ModuleDefinition module)
        {
            throw new NotImplementedException();
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
                        DataType = null //field.Type, problema os tipos não são compatíveis
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
            throw new NotImplementedException();
        }

        private List<ImportModel> GenerateImports(ModuleDefinition module, IEnumerable<ImportDefinition> importDefinitions)
        {
            throw new NotImplementedException();
        }

        private List<InterfaceImportModel> GenerateInterfaces(ModuleDefinition module)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
