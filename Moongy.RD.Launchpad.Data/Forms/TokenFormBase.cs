using Moongy.RD.Launchpad.Core.Enums;

namespace Moongy.RD.Launchpad.Data.Forms
{
    public abstract class TokenBaseModel
    {
        public required string Name { get; set; }
        public required string Symbol { get; set; }
        public required SpdxLicense License { get; set; }
        public required string SecurityContact { get; set; }
    }
}
