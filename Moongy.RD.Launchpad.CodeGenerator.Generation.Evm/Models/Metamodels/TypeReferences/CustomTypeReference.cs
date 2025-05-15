using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Enums;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.TypeReferences;

public class CustomTypeReference(string customTypeName) : TypeReference(SolidityDataTypeEnum.Custom)
{
    public string CustomTypeName { get; init; } = customTypeName;
}