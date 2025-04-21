using Moongy.RD.Launchpad.Core.Enums;

namespace Moongy.RD.Launchpad.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MetaModelPropertyAttribute : Attribute
    {
        public string Name { get; set; } = "";
        public PropertyType PropertyType { get; set; }
        public DataType DataType { get; set; }
        public VisibilityModifier Visibility { get; set; } = VisibilityModifier.None;
    }

}
