using System.Reflection;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Attributes;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Core.Validators;
public static class TokenomicsValidator
{
    public static void ValidateWeight(List<ITokenomic> tokenomics)
    {
        double weight = 0;
        foreach(var tokenomic in tokenomics)
        {
            var tokenomicAttribute = tokenomic.GetType().GetCustomAttribute<TokenomicAttribute>();
            if (tokenomicAttribute == null) continue;
            weight +=
                tokenomic.TriggerMode == Enums.TokenomicTriggerMode.Automatic ?
                tokenomicAttribute.Weight :
                CalcTokenomicWeight(tokenomic, tokenomics);
            if (weight > 100) throw new TokenExceedsTokenomicWeighLimitException();
        }
    }

    public static void ValidateTax(List<ITokenomic> tokenomics, double maxTax)
    {
        double tax = 0;
        foreach(var tokenomic in tokenomics)
        {
            tax += tokenomic.TaxPercentage;
            if (tax > maxTax) throw new TokenomicsExceedMaxTaxException();
        }
    }

    public static void Validate(List<ITokenomic> tokenomics, double maxTax)
    {
        ValidateWeight(tokenomics);
        ValidateTax(tokenomics, maxTax);
    }
}
