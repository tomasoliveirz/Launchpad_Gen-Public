using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.TypeReferences;

public abstract class TypeReference(SolidityDataTypeEnum baseType) : SolidityModel
{
    public SolidityDataTypeEnum BaseType { get; init; } = baseType;
}