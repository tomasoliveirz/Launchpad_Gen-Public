using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Enums;

namespace Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TokenomicAttribute : Attribute
    {
        public required TokenomicEnum Source { get; set; }
    }
}
