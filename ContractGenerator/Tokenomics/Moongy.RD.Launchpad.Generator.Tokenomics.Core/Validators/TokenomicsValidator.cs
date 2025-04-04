using System.Reflection;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Attributes;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Enums;
using Moongy.RD.Launchpad.Core.Exceptions;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Core.Validators
{
    public static class TokenomicsValidator
    {
        public static void ValidateWeight(List<ITokenomic> tokenomics)
        {
            double totalWeight = 0;
            foreach (var tokenomic in tokenomics)
            {
                var tokenomicAttribute = tokenomic.GetType().GetCustomAttribute<TokenomicAttribute>();
                if (tokenomicAttribute == null) 
                    continue;

                if (tokenomic.TriggerMode == TokenomicTriggerMode.Automatic)
                {
                    totalWeight += tokenomicAttribute.Weight;
                }
                else
                {
                    totalWeight += CalcManualModeWeight(tokenomic, tokenomics);
                }

                if (totalWeight > 100)
                    throw new TokenWeightExceededException(totalWeight, 100);
            }
        }
        
        public static void ValidateTax(List<ITokenomic> tokenomics, double maxTax)
        {
            double totalTax = 0;
            foreach (var tokenomic in tokenomics)
            {
                totalTax += tokenomic.TaxPercentage;
                if (totalTax > maxTax)
                    throw new TokenTaxExceededException(totalTax, maxTax);
            }
        }

        public static void Validate(List<ITokenomic> tokenomics, double maxTax)
        {
            ValidateWeight(tokenomics);
            ValidateTax(tokenomics, maxTax);
        }
        
        private static double CalcManualModeWeight(ITokenomic tokenomic, List<ITokenomic> allTokenomics)
        {
            double totalAdditionalWeight = 0.0;

            var attr = tokenomic.GetType().GetCustomAttribute<TokenomicAttribute>();
            double baseWeight = attr!.Weight;
            
            bool hasTransfer = HasAssociatedTransfer(tokenomic, allTokenomics);

            if (hasTransfer)
            {
                totalAdditionalWeight += baseWeight;
                totalAdditionalWeight += 5.0; // exemplo por agora
            }
            else
            {
                totalAdditionalWeight += 3.0; // examplo por agora
            }

            return totalAdditionalWeight;
        }
        
        private static bool HasAssociatedTransfer(ITokenomic tokenomic, List<ITokenomic> allTokenomics)
        {
            return false;
        }

    }
}
