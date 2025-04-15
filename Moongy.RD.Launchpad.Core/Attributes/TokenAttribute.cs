using Moongy.RD.Launchpad.Core.Enums;

namespace Moongy.RD.Launchpad.Core.Attributes
{
    public class TokenAttribute : Attribute
    {
        public string? Name { get; set; }
        public TokenClassification[] Tags { get; set; } = [];
    }
}
