using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ContextPropertyAttribute : Attribute
    {
        public PrimitiveType Type { get; set; }
        public string? Name { get; set; }
        public Visibility Visibility { get; set; }
        public bool HasDefaultValue { get; set; }
    }
}
