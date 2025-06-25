using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Helpers;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Parameters;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.TypeReferences;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.ScribanRenderingModels;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Processors;

public class ConstructorProcessor() : BaseSolidityTemplateProcessor<SolidityContractModel>("ContractConstructor")
{
    public override string Render(SolidityContractModel model)
    {
        try
        {
            Validate(model);

            var renderModel = Transform(model);
            var parameters = string.Join(", ", renderModel.Arguments);
            var baseConstructors = renderModel.BaseConstructors.Count > 0
                ? " : " + string.Join(", ", renderModel.BaseConstructors)
                : "";

            return Render(new
            {
                parameters,
                baseConstructors,
                assignments = renderModel.Assignments
            });
        }
        catch (Exception ex)
        {
            return $"constructor() {{ /* Error: {ex.Message} */ }}";
        }
    }

    private static ConstructorRenderingModel Transform(SolidityContractModel model)
    {
        var arguments = TransformArguments(model);
        var baseConstructors = TransformBaseConstructors(model);
        var assignments = TransformAssignments(model);

        return new()
        {
            Arguments = arguments,
            BaseConstructors = baseConstructors,
            Assignments = assignments
        };
    }

    private static List<string> TransformAssignments(SolidityContractModel model)
    {
        var assignments = new List<string>();

        foreach (var param in model.ConstructorParameters)
        {
            if (string.IsNullOrEmpty(param.AssignedTo))
                continue;

            var stateProp = model.StateProperties.FirstOrDefault(sp => sp.Name == param.AssignedTo);
            if (stateProp == null)
                throw new ArgumentException($"Constructor parameter '{param.Name}' is assigned to non-existent state property '{param.AssignedTo}'.");

            if (stateProp.IsConstant)
                throw new ArgumentException($"Cannot assign to constant state variable '{stateProp.Name}'.");

            assignments.Add($"{param.AssignedTo} = {param.Name};");
        }

        if (model.ConstructorStatements != null)
        {
            assignments.AddRange(model.ConstructorStatements);
        }

        return assignments;
    }

    private static List<string> TransformBaseConstructors(SolidityContractModel model)
    {
        if (model.BaseContracts.Count == 0)
            return [];

        var processedConstructors = new HashSet<string>();
        var result = new List<string>();

        foreach (var bc in model.BaseContracts)
        {
            if (processedConstructors.Contains(bc.Name))
                continue;

            processedConstructors.Add(bc.Name);

            var arguments = bc.ConstructorParameters
                .OrderBy(p => p.Index)
                .Select(p => FormatArgumentValue(p.Value ?? p.Name, p.Type))
                .ToList();

            var argumentsString = string.Join(", ", arguments);
            result.Add($"{bc.Name}({argumentsString})");
        }

        return result;
    }

    private static string FormatArgumentValue(string value, TypeReference type)
    {
        if (type is SimpleTypeReference simpleType &&
            simpleType.BaseType == SolidityDataTypeEnum.String &&
            !value.StartsWith("\"") && !value.EndsWith("\""))
        {
            return $"\"{value}\"";
        }

        return value;
    }

    private static string DetermineMemoryLocation(ConstructorParameterModel param)
    {
        return param.Location switch
        {
            SolidityMemoryLocation.Memory => " memory",
            SolidityMemoryLocation.Calldata => " calldata",
            SolidityMemoryLocation.Storage => " storage",
            _ => ""
        };
    }

    private static List<string> TransformArguments(SolidityContractModel model)
    {
        return [.. model.ConstructorParameters
            .OrderBy(p => p.Index)
            .Select(p =>
            {
                var typeString = SolidityReferenceTypeSyntaxHelper.RenderTypeReference(p.Type);
                var locationString = DetermineMemoryLocation(p);
                return $"{typeString}{locationString} {p.Name}";
            })];
    }

    private void Validate(SolidityContractModel model)
    {
        var paramNames = model.ConstructorParameters.Select(p => p.Name).ToList();
        if (paramNames.Count != paramNames.Distinct().Count())
        {
            throw new ArgumentException("Constructor has duplicate parameter names.");
        }

        var immutableAssignments = new Dictionary<string, int>();
        foreach (var prop in model.StateProperties.Where(p => p.IsImmutable))
        {
            immutableAssignments[prop.Name] = 0;
        }

        foreach (var param in model.ConstructorParameters)
        {
            ValidateDataLocation(param);

            if (!string.IsNullOrEmpty(param.AssignedTo))
            {
                var stateProp = model.StateProperties.FirstOrDefault(sp => sp.Name == param.AssignedTo);
                if (stateProp != null)
                {
                    if (stateProp.IsConstant)
                    {
                        throw new ArgumentException($"Cannot assign to constant state variable '{stateProp.Name}'.");
                    }

                    if (stateProp.IsImmutable && immutableAssignments.ContainsKey(stateProp.Name))
                    {
                        immutableAssignments[stateProp.Name]++;
                    }

                    if (!TypesAreCompatible(param.Type, stateProp.Type))
                    {
                        throw new ArgumentException($"Constructor parameter '{param.Name}' type is not compatible with state property '{param.AssignedTo}' type.");
                    }
                }
            }
        }

        foreach (var kvp in immutableAssignments)
        {
            if (kvp.Value > 1)
            {
                throw new ArgumentException($"Immutable state variable '{kvp.Key}' is assigned multiple times.");
            }
        }

        ValidateBaseConstructors(model);
    }

    private void ValidateDataLocation(ConstructorParameterModel param)
    {

        if (param.Type is MappingTypeReference)
        {
            throw new ArgumentException($"Constructor parameter '{param.Name}' cannot be mapping type.");
        }

        bool requiresLocation = RequiresMemoryLocation(param.Type);

        if (requiresLocation)
        {
            if (!param.Location.HasValue || param.Location == SolidityMemoryLocation.None)
            {
                if (param.Type is SimpleTypeReference simpleType)
                {
                    Console.WriteLine($"   SimpleType BaseType: {simpleType.BaseType}");
                }
                
                throw new ArgumentException($"Parameter '{param.Name}' must specify a memory location.");
            }
            
            if (param.Location != SolidityMemoryLocation.Memory && param.Location != SolidityMemoryLocation.Calldata)
            {
                throw new ArgumentException($"Constructor parameter '{param.Name}' must use 'memory' or 'calldata'.");
            }
        }
        else if (param.Location.HasValue && param.Location.Value != SolidityMemoryLocation.None)
        {
            throw new ArgumentException($"Parameter '{param.Name}' is a value type and should not specify memory location.");
        }

    }

    private static bool RequiresMemoryLocation(TypeReference type)
    {
        if (type is MappingTypeReference)
        {
            return false;
        }

        if (type is ArrayTypeReference)
        {
            return true;
        }

        if (type is SimpleTypeReference simpleType)
        {
            bool result = simpleType.BaseType == SolidityDataTypeEnum.String ||
                          simpleType.BaseType == SolidityDataTypeEnum.Bytes;
            return result;
        }

        if (type is CustomTypeReference)
        {
            return true;
        }

        return false;
    }

    private bool TypesAreCompatible(TypeReference sourceType, TypeReference targetType)
    {
        if (sourceType.GetType() == targetType.GetType())
        {
            if (sourceType is SimpleTypeReference simpleSource && targetType is SimpleTypeReference simpleTarget)
            {
                return simpleSource.BaseType == simpleTarget.BaseType;
            }
            else if (sourceType is CustomTypeReference customSource && targetType is CustomTypeReference customTarget)
            {
                return customSource.CustomTypeName == customTarget.CustomTypeName;
            }
            else if (sourceType is ArrayTypeReference arraySource && targetType is ArrayTypeReference arrayTarget)
            {
                return TypesAreCompatible(arraySource.ElementType, arrayTarget.ElementType);
            }
            else if (sourceType is MappingTypeReference mappingSource && targetType is MappingTypeReference mappingTarget)
            {
                return TypesAreCompatible(mappingSource.KeyType, mappingTarget.KeyType) &&
                       TypesAreCompatible(mappingSource.ValueType, mappingTarget.ValueType);
            }
        }

        return false;
    }

    private void ValidateBaseConstructors(SolidityContractModel model)
    {
        foreach (var baseContract in model.BaseContracts)
        {
            foreach (var baseParam in baseContract.ConstructorParameters)
            {
                if (!string.IsNullOrEmpty(baseParam.Value))
                {
                    ValidateArgumentValue(baseParam.Value, baseParam.Type);
                }
                else
                {
                    var ourParam = model.ConstructorParameters.FirstOrDefault(p => p.Name == baseParam.Name);
                    if (ourParam == null)
                    {
                        throw new ArgumentException($"Base contract '{baseContract.Name}' constructor parameter '{baseParam.Name}' references non-existent parameter.");
                    }

                    if (!TypesAreCompatible(ourParam.Type, baseParam.Type))
                    {
                        throw new ArgumentException($"Base contract '{baseContract.Name}' constructor parameter '{baseParam.Name}' is not type compatible with parameter of the same name.");
                    }
                }
            }
        }
    }

    private void ValidateArgumentValue(string value, TypeReference type)
    {
        if (type is SimpleTypeReference simpleType)
        {
            switch (simpleType.BaseType)
            {
                case SolidityDataTypeEnum.Bool:
                    if (value != "true" && value != "false")
                    {
                        throw new ArgumentException($"Value '{value}' is not a valid boolean.");
                    }
                    break;
                case SolidityDataTypeEnum.Uint256:
                case SolidityDataTypeEnum.Uint128:
                case SolidityDataTypeEnum.Uint64:
                case SolidityDataTypeEnum.Uint32:
                case SolidityDataTypeEnum.Uint16:
                case SolidityDataTypeEnum.Uint8:
                    if (!ulong.TryParse(value, out _))
                    {
                        throw new ArgumentException($"Value '{value}' is not a valid unsigned integer.");
                    }
                    break;
                case SolidityDataTypeEnum.Int256:
                case SolidityDataTypeEnum.Int128:
                case SolidityDataTypeEnum.Int64:
                case SolidityDataTypeEnum.Int32:
                case SolidityDataTypeEnum.Int16:
                case SolidityDataTypeEnum.Int8:
                    if (!long.TryParse(value, out _))
                    {
                        throw new ArgumentException($"Value '{value}' is not a valid integer.");
                    }
                    break;
                case SolidityDataTypeEnum.Address:
                    if (!value.StartsWith("0x") || value.Length != 42)
                    {
                        throw new ArgumentException($"Value '{value}' is not a valid Ethereum address.");
                    }
                    break;
            }
        }
    }
}