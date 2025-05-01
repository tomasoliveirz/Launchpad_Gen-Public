namespace Moongy.RD.Launchpad.Core.Attributes;


[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class EnumLabelAttribute : Attribute
{
    public string? Display { get; set; }
    public string? Description { get; set; }
    public string? Value { get; set; }
}
