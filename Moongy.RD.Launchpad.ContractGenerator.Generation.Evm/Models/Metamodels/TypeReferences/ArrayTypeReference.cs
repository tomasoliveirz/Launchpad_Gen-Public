using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;
namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.TypeReferences;

public class ArrayTypeReference(TypeReference elementType) : TypeReference(SolidityDataTypeEnum.Array)
{
    public TypeReference ElementType { get; init; } = elementType;
    
}