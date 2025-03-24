using Moongy.RD.Launchpad.Core.Models;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.AntiWhale.Models;
public class AntiWhaleTokenomicModel : BaseTokenomicModel
{
    public decimal MaxWalletPercentage { get; set; }
    public List<Address>? NotAplicableAddresses { get; set; }
}
