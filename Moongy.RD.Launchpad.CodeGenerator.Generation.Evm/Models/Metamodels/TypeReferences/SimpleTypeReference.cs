using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Enums;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.TypeReferences;

public class SimpleTypeReference : TypeReference
{
    public SimpleTypeReference(SolidityDataTypeEnum baseType)
        : base(baseType)
    {
        if (baseType == SolidityDataTypeEnum.Array ||
            baseType == SolidityDataTypeEnum.Mapping ||
            baseType == SolidityDataTypeEnum.Tuple ||
            baseType == SolidityDataTypeEnum.Custom)
            throw new ArgumentException("Use appropriate derived classes for Array, Mapping, or Custom types.");
    }
}