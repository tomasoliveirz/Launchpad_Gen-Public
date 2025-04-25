using Moongy.RD.Launchpad.Core.Constants;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Support;
using Moongy.RD.Launchpad.Generator.Tokenomics.Tax.Models;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Tax.Validators;
public class TaxWeighter : IWeightedTokenomic<TaxTokenomicModel>
{
    public double GetWeight(TaxTokenomicModel model)
    {
        var currentWeight = Tokenomic.GetTokenomicWeight(model);
        if(model.TriggerMode == Core.Enums.TokenomicTriggerMode.Manual && model.TaxCollector != AddressConstants.ZeroAddress)
        {
            return currentWeight;
        }
        return currentWeight * model.TaxRecipients.Count;
    }
}
