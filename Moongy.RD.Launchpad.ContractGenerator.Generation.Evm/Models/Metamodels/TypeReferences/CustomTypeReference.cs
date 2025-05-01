using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.TypeReferences;

public class CustomTypeReference(string customTypeName) : TypeReference(SolidityDataTypeEnum.Custom)
{
    public string CustomTypeName { get; init; } = customTypeName;
}