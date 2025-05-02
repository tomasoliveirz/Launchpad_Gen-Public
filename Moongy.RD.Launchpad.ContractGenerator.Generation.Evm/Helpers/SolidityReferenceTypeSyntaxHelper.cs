using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Expressions;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.TypeReferences;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Helpers;

public class SolidityReferenceTypeSyntaxHelper : BaseSoliditySyntaxHelper<TypeReference>
{
    private static readonly Dictionary<SolidityDataTypeEnum, string> SolidityDataType = new()
    {
        { SolidityDataTypeEnum.Uint256, SolidityDataTypes.Uint256 },
        { SolidityDataTypeEnum.Uint128, SolidityDataTypes.Uint128 },
        { SolidityDataTypeEnum.Uint64, SolidityDataTypes.Uint64 },
        { SolidityDataTypeEnum.Uint32, SolidityDataTypes.Uint32 },
        { SolidityDataTypeEnum.Uint16, SolidityDataTypes.Uint16 },
        { SolidityDataTypeEnum.Uint8, SolidityDataTypes.Uint8 },
        { SolidityDataTypeEnum.Int256, SolidityDataTypes.Int256 },
        { SolidityDataTypeEnum.Int128, SolidityDataTypes.Int128 },
        { SolidityDataTypeEnum.Int64, SolidityDataTypes.Int64 },
        { SolidityDataTypeEnum.Int32, SolidityDataTypes.Int32 },
        { SolidityDataTypeEnum.Int16, SolidityDataTypes.Int16 },
        { SolidityDataTypeEnum.Int8, SolidityDataTypes.Int8 },
        { SolidityDataTypeEnum.Address, SolidityDataTypes.Address },
        { SolidityDataTypeEnum.Bool, SolidityDataTypes.Bool },
        { SolidityDataTypeEnum.String, SolidityDataTypes.String },
        { SolidityDataTypeEnum.Bytes, SolidityDataTypes.Bytes }
    };

    public override string Render(TypeReference typeReference)
    {
        return RenderTypeReference(typeReference);
    }

    private static string RenderSimpleType(SimpleTypeReference typeReference)
    {
        return SolidityDataType[typeReference.BaseType];
    }

    private static string RenderArrayType(ArrayTypeReference typeReference)
    {
        return RenderTypeReference(typeReference.ElementType) + "[]";
    }

    private static string RenderMappingType(MappingTypeReference typeReference)
    {
        var keyType = RenderTypeReference(typeReference.KeyType);
        var valueType = RenderTypeReference(typeReference.ValueType);
        var mapping = WrapParameters([keyType, SoliditySymbols.KeyValueSeparator, valueType]);
        return $"{SolidityDataTypes.Mapping}{mapping}";
    }

    public static string RenderTypeReference(TypeReference typeReference)
    {
        return typeReference switch
        {
            SimpleTypeReference simple => RenderSimpleType(simple),
            CustomTypeReference custom => custom.CustomTypeName,
            ArrayTypeReference array => RenderArrayType(array),
            MappingTypeReference mapping => RenderMappingType(mapping),
            _ => throw new NotSupportedException("Unknown type reference")
        };
    }
}
