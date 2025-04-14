using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Attributes;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Enums;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Tax.Models;
[Tokenomic(Weight=20)]
public class TaxTokenomicModel : BaseTokenomicModel
{
    public List<TaxRecipient> TaxRecipients { get; set; } = [];
}
