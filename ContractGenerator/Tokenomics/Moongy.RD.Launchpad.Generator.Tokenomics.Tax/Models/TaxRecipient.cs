using Moongy.RD.Launchpad.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Tax.Models
{
    public class TaxRecipient
    {
        public Address? Address { get; set; }
        public decimal Shares { get; set; }
    }
}
