namespace Moongy.RD.Launchpad.Generator.Tokenomics.Core.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class TokenomicAttribute : Attribute
{
    public double Weight { get; set; }
    public Type? WeighterType { get; set; }

}
