using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.TypeReferences;

public class MappingTypeReference(TypeReference keyType, TypeReference valueType) : TypeReference(SolidityDataTypeEnum.Mapping)
{
    public TypeReference KeyType { get; init; } = keyType ?? throw new ArgumentNullException(nameof(keyType));
    public TypeReference ValueType { get; init; } = valueType ?? throw new ArgumentNullException(nameof(valueType));
}