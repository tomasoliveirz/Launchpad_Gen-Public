using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base
{
    public static class DataTypeReference
    {
        public static TypeReference Address => new TypeReference() { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Address };
        public static TypeReference Bool => new TypeReference() { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Bool };
        public static TypeReference Int8 => new TypeReference() { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Int8 };
        public static TypeReference Int16 => new TypeReference() { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Int16 };
        public static TypeReference Int32 => new TypeReference() { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Int32 };
        public static TypeReference Int64 => new TypeReference() { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Int64 };
        public static TypeReference Int128 => new TypeReference() { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Int128 };
        public static TypeReference Uint8 => new TypeReference() { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Uint8 };
        public static TypeReference Uint16 => new TypeReference() { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Uint16 };
        public static TypeReference Uint32 => new TypeReference() { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Uint32 };
        public static TypeReference Uint64 => new TypeReference() { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Uint64 };
        public static TypeReference Uint128 => new TypeReference() { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Uint128 };
        public static TypeReference Uint256 => new TypeReference() { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Uint256 };
        public static TypeReference String => new TypeReference() { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.String };
        public static TypeReference Bytes => new TypeReference() { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Bytes };
    }
}
