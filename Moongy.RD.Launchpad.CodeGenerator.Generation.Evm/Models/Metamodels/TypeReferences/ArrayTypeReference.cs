using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Enums;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.TypeReferences;

public class ArrayTypeReference(TypeReference elementType) : TypeReference(SolidityDataTypeEnum.Array)
{
    public TypeReference ElementType { get; init; } = elementType;
    
}