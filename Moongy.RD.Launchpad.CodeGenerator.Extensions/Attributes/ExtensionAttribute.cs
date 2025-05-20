using Moongy.RD.Launchpad.CodeGenerator.Extensions.Enums;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public abstract class ExtensionAttribute : Attribute
{
    public required ExtensionEnum Source {get ; set; }
}
