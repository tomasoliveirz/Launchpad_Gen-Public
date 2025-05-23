using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base
{
    public static class DataTypeReference
    {
        public static TypeReference Address => new TypeReference() { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Address };
        public static TypeReference Uint256 => new TypeReference() { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Uint256 };
        public static TypeReference String => new TypeReference() { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.String };
        public static TypeReference Bool => new TypeReference() { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Bool };
    }
}
