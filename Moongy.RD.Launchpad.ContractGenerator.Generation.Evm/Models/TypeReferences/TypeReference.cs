using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Base;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.TypeReferences;

public abstract class TypeReference(SolidityDataTypeEnum baseType) : SolidityModel
{
    public SolidityDataTypeEnum BaseType { get; init; } = baseType;
}