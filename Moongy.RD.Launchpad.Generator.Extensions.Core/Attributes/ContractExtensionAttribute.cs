namespace Moongy.RD.Launchpad.Generator.Extensions.Core.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ContractExtensionAttribute : Attribute
{
    public double Weight { get; set; }
}
