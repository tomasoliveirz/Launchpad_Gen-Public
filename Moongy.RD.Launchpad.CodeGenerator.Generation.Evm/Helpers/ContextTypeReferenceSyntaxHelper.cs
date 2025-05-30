using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.TypeReferences;
using CoreTypeReference = Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others.TypeReference;
using SolidityTypeReference = Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.TypeReferences.TypeReference;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Helpers;

public class ContextTypeReferenceSyntaxHelper
{
    private static readonly Dictionary<PrimitiveType, SolidityDataTypeEnum> PrimitiveToSolidityMapping = new()
    {
        { PrimitiveType.Bool, SolidityDataTypeEnum.Bool },
        { PrimitiveType.String, SolidityDataTypeEnum.String },
        { PrimitiveType.Bytes, SolidityDataTypeEnum.Bytes },
        { PrimitiveType.Address, SolidityDataTypeEnum.Address },
        { PrimitiveType.Uint8, SolidityDataTypeEnum.Uint8 },
        { PrimitiveType.Uint16, SolidityDataTypeEnum.Uint16 },
        { PrimitiveType.Uint32, SolidityDataTypeEnum.Uint32 },
        { PrimitiveType.Uint64, SolidityDataTypeEnum.Uint64 },
        { PrimitiveType.Uint128, SolidityDataTypeEnum.Uint128 },
        { PrimitiveType.Uint256, SolidityDataTypeEnum.Uint256 },
        { PrimitiveType.Int8, SolidityDataTypeEnum.Int8 },
        { PrimitiveType.Int16, SolidityDataTypeEnum.Int16 },
        { PrimitiveType.Int32, SolidityDataTypeEnum.Int32 },
        { PrimitiveType.Int64, SolidityDataTypeEnum.Int64 },
        { PrimitiveType.Int128, SolidityDataTypeEnum.Int128 }
    };
    public static SolidityTypeReference MapToSolidityTypeReference(CoreTypeReference coreTypeReference)
    {
        return coreTypeReference.Kind switch
        {
            TypeReferenceKind.Simple => MapSimpleType(coreTypeReference),
            TypeReferenceKind.Custom => MapCustomType(coreTypeReference),
            TypeReferenceKind.Array => MapArrayType(coreTypeReference),
            TypeReferenceKind.Mapping => MapMappingType(coreTypeReference),
            TypeReferenceKind.Tuple => MapTupleType(coreTypeReference),
            _ => throw new NotSupportedException($"TypeReferenceKind '{coreTypeReference.Kind}' is not supported for Solidity mapping")
        };
    }
    private static SimpleTypeReference MapSimpleType(CoreTypeReference coreTypeReference)
    {
        SolidityDataTypeEnum solidityType;

        if (coreTypeReference.Primitive != PrimitiveType.None)
        {
            if (PrimitiveToSolidityMapping.TryGetValue(coreTypeReference.Primitive, out solidityType))
            {
                return new SimpleTypeReference(solidityType);
            }

            throw new NotSupportedException($"PrimitiveType '{coreTypeReference.Primitive}' is not supported in Solidity");
        }

        if (!string.IsNullOrEmpty(coreTypeReference.TypeName))
        {
            solidityType = coreTypeReference.TypeName.ToLowerInvariant() switch
            {
                "int256" => SolidityDataTypeEnum.Int256,
                "uint48" => SolidityDataTypeEnum.Uint48,
                "bytes4" => SolidityDataTypeEnum.Bytes4,
                _ => throw new NotSupportedException($"Named type '{coreTypeReference.TypeName}' is not a recognized Solidity primitive type")
            };

            return new SimpleTypeReference(solidityType);
        }

        throw new NotSupportedException("Simple type reference must have either a primitive type or type name");
    }
    private static CustomTypeReference MapCustomType(CoreTypeReference coreTypeReference)
    {
        if (string.IsNullOrEmpty(coreTypeReference.TypeName))
        {
            throw new ArgumentException("Custom type reference must have a TypeName", nameof(coreTypeReference));
        }

        return new CustomTypeReference(coreTypeReference.TypeName);
    }

    private static ArrayTypeReference MapArrayType(CoreTypeReference coreTypeReference)
    {
        if (coreTypeReference.ElementType == null)
        {
            throw new ArgumentException("Array type reference must have an ElementType", nameof(coreTypeReference));
        }

        var elementType = MapToSolidityTypeReference(coreTypeReference.ElementType);
        return new ArrayTypeReference(elementType);
    }
    private static MappingTypeReference MapMappingType(CoreTypeReference coreTypeReference)
    {
        if (coreTypeReference.KeyType == null)
        {
            throw new ArgumentException("Mapping type reference must have a KeyType", nameof(coreTypeReference));
        }

        if (coreTypeReference.ValueType == null)
        {
            throw new ArgumentException("Mapping type reference must have a ValueType", nameof(coreTypeReference));
        }

        var keyType = MapToSolidityTypeReference(coreTypeReference.KeyType);
        var valueType = MapToSolidityTypeReference(coreTypeReference.ValueType);

        return new MappingTypeReference(keyType, valueType);
    }

    private static SolidityTypeReference MapTupleType(CoreTypeReference coreTypeReference)
    {
        if (coreTypeReference.ElementTypes == null || coreTypeReference.ElementTypes.Count == 0)
        {
            throw new ArgumentException("Tuple type reference must have ElementTypes", nameof(coreTypeReference));
        }

        var elementTypes = coreTypeReference.ElementTypes
            .Select(MapToSolidityTypeReference)
            .ToArray();

        return new TupleTypeReference(elementTypes);
    }

        private static readonly Dictionary<Visibility, SolidityVisibilityEnum> VisibilityToSolidityMapping = new()
    {
        { Visibility.Public, SolidityVisibilityEnum.Public },
        { Visibility.Private, SolidityVisibilityEnum.Private },
        { Visibility.Internal, SolidityVisibilityEnum.Internal },
        { Visibility.Protected, SolidityVisibilityEnum.Internal } 
    };

    public static SolidityVisibilityEnum MapToSolidityVisibility(Visibility visibility)
    {
        if (VisibilityToSolidityMapping.TryGetValue(visibility, out var solidityVisibility))
        {
            return solidityVisibility;
        }

        throw new NotSupportedException($"Visibility '{visibility}' is not supported for Solidity mapping");
    }

}