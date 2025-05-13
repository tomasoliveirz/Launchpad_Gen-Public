namespace Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others
{
    public class TypeReference
    {
        public TypeReferenceKind Kind { get; set; }

        #region Simple
        public PrimitiveType Primitive { get; set; } = PrimitiveType.None;
        public string? TypeName { get; set; }
        #endregion

        #region Array
        public TypeReference? ElementType { get; set; }
        #endregion

        #region Mapping
        public TypeReference? KeyType { get; set; }
        public TypeReference? ValueType { get; set; }
        #endregion

        #region Tuple
        public List<TypeReference> ElementTypes { get; set; } = [];
        #endregion

        public List<TypeReference> GenericArguments { get; set; } = [];

    }
}
