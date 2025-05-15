namespace Moongy.RD.Launchpad.CodeGenerator.Core.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public abstract class CodePropertyAttribute<T> : Attribute
{
    public required T Source { get; set; }
    public string? Name { get; set; }
}