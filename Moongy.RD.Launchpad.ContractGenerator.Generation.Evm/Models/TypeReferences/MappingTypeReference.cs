using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.TypeReferences;

public class MappingTypeReference : TypeReference
{
    public TypeReference KeyType { get; init; }
    public TypeReference ValueType { get; init; }

    public MappingTypeReference(TypeReference keyType, TypeReference valueType)
        : base(SolidityDataTypeEnum.Mapping)
    {
        KeyType = keyType ?? throw new ArgumentNullException(nameof(keyType));
        ValueType = valueType ?? throw new ArgumentNullException(nameof(valueType));
    }
}