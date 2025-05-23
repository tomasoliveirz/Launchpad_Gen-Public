namespace Moongy.RD.Launchpad.CodeGenerator.Core.Helpers;

using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;


// each time we needed a type reference we had to create repetitive blocks as new TypeReference { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Uint16 }
// we also created a lot of handwritten code to create arrays and mappings
// so this class is a helper to create type references in a more readable way
public static class TypeRef
{
    public static TypeReference Primitive(PrimitiveType p) =>
        new()
        {
            Kind      = TypeReferenceKind.Simple,
            Primitive = p
        };
    
    public static TypeReference ArrayOf(PrimitiveType element) =>
        ArrayOf(Primitive(element));

    public static TypeReference ArrayOf(TypeReference element) =>
        new()
        {
            Kind        = TypeReferenceKind.Array,
            ElementType = element
        };
    
    public static TypeReference Mapping(TypeReference key, TypeReference value) =>
        new()
        {
            Kind      = TypeReferenceKind.Mapping,
            KeyType   = key,
            ValueType = value
        };

    public static TypeReference Mapping(PrimitiveType key, PrimitiveType val) =>
        Mapping(Primitive(key), Primitive(val));
}