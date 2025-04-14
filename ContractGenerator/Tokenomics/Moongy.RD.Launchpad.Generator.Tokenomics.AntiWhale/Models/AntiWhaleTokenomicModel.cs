using Moongy.RD.Launchpad.Core.Models;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Attributes;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.AntiWhale.Models;
[Tokenomic(Weight=15)]
public class AntiWhaleTokenomicModel : BaseTokenomicModel
{
    public decimal MaxWalletPercentage { get; set; }
    public List<Address> NotAplicableAddresses { get; set;} = [];
}
