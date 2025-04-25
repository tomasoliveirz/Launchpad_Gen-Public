namespace Moongy.RD.Launchpad.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MetaModelNameAttribute : Attribute
    {
        public string? Name { get; set; }
    }

}
