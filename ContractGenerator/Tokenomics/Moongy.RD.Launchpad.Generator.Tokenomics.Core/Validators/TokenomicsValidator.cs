using System.Reflection;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Attributes;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Enums;
using Moongy.RD.Launchpad.Core.Exceptions;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Core.Validators
{
    public static class TokenomicsValidator
    {
        private const double MAX_TOTAL_TAX_PERCENTAGE = 20.0;
        private const double MAX_TOTAL_WEIGHT = 100.0;
        
        
        // dicionario dos pesos a serem usados no manual mode
        private static readonly Dictionary<string, double> OptimizedWeights = new()
        {
            { "TaxTokenomicModel", 20 },
            { "DeflationTokenomicModel", 20 },
            { "LiquidityGenerationTokenomicModel", 20 },
            { "BuybackTokenomicModel", 20 },
            { "ReflectionsTokenomicModel", 40 },
            { "AntiWhaleTokenomicModel", 15 }
        };
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
                    totalWeight += CalcManualModeWeight(tokenomic);
                }

                if (totalWeight > MAX_TOTAL_WEIGHT)
                    throw new TokenWeightExceededException(totalWeight, MAX_TOTAL_WEIGHT);
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
            ValidateCompatibility(tokenomics);
            ValidateWeight(tokenomics);
            ValidateTax(tokenomics, maxTax);
        }
        
        public static void ValidateCompatibility(List<ITokenomic> tokenomics)
        {   
            bool hasReflection = tokenomics.Any(t => t.GetType().Name.Contains("Reflection"));
            bool hasBuyback = tokenomics.Any(t => t.GetType().Name.Contains("Buyback"));
            bool hasLiquidity = tokenomics.Any(t => t.GetType().Name.Contains("Liquidity"));
            
            if (hasReflection && hasBuyback && hasLiquidity)
                throw new InvalidTokenomicException("Using Reflections, Buyback, and Liquidity together exceeds recommended computational limits.");
            
            if (hasReflection && tokenomics.Count > 3 && tokenomics.Any(t => t.TriggerMode == TokenomicTriggerMode.Automatic))
                throw new InvalidTokenomicException("Reflections in automatic mode with more than 3 tokenomics may exceed computational limits. Consider using manual mode for Reflections.");
                    
            int taxingTokenomics = tokenomics.Count(t => t.TaxPercentage > 0);
            if (taxingTokenomics >= 3 && tokenomics.Sum(t => t.TaxPercentage) > 15)
                throw new InvalidTokenomicException("Multiple tokenomics with taxation approaching 20% may impact user experience negatively. Consider reducing tax percentages or using manual mode.");
        }
        
        private static double CalcManualModeWeight(ITokenomic tokenomic)
        {
            string tokenomicTypeName = tokenomic.GetType().Name;
    
            var attr = tokenomic.GetType().GetCustomAttribute<TokenomicAttribute>();
            double baseWeight = attr.Weight;
    
            if (tokenomicTypeName == "TaxTokenomicModel")
            {
                return 20.0;
            }
            
            if (tokenomicTypeName == "LiquidityGenerationTokenomicModel")
            {
                return 20.0;
            }
            
            if (tokenomicTypeName == "BuybackTokenomicModel")
            {
                return 20.0;
            }
            
            if (tokenomicTypeName == "ReflectionsTokenomicModel")
            {
                return 40.0;
            }

            return baseWeight;
        }
        
        private static bool HasAssociatedTransfer(ITokenomic tokenomic, List<ITokenomic> allTokenomics)
        {
            string tokenomicName = tokenomic.GetType().Name;
            
            if (tokenomicName.Contains("Reflection"))
            {
                return allTokenomics.Any(t => t.GetType().Name.Contains("Tax") || 
                                            t.GetType().Name.Contains("Liquidity"));
            }
            
            if (tokenomicName.Contains("Buyback"))
            {
                return allTokenomics.Any(t => t.GetType().Name.Contains("Liquidity"));
            }
            
            return false;
        }

        public static void ValidateIndividualTokenomic(ITokenomic tokenomic)
        {
            if (tokenomic == null)
                throw new ArgumentNullException(nameof(tokenomic));
                    
            if (tokenomic.TaxPercentage < 0 || tokenomic.TaxPercentage > 100)
                throw new InvalidTokenomicException(
                    $"{tokenomic.GetType().Name}: TaxPercentage must be between 0 and 100.");
            
            string typeName = tokenomic.GetType().Name;
            
            try
            {
                switch (typeName)
                {
                    case "TaxTokenomicModel":
                        dynamic taxModel = tokenomic;
                        dynamic taxValidator = Type.GetType("Moongy.RD.Launchpad.Generator.Tokenomics.Tax.Validators.TaxTokenomicValidator, Moongy.RD.Launchpad.Generator.Tokenomics.Tax");
                        taxValidator.Validate(taxModel);
                        break;
                        
                    case "DeflationTokenomicModel":
                        dynamic deflationModel = tokenomic;
                        dynamic deflationValidator = Type.GetType("Moongy.RD.Launchpad.Generator.Tokenomics.Deflation.Validators.DeflationTokenomicValidator, Moongy.RD.Launchpad.Generator.Tokenomics.Deflation");
                        deflationValidator.Validate(deflationModel);
                        break;
                        
                    case "LiquidityGenerationTokenomicModel":
                        dynamic liquidityModel = tokenomic;
                        dynamic liquidityValidator = Type.GetType("Moongy.RD.Launchpad.Generator.Tokenomics.LiquidityGeneration.Validators.LiquidityGenerationTokenomicValidator, Moongy.RD.Launchpad.Generator.Tokenomics.LiquidityGeneration");
                        liquidityValidator.Validate(liquidityModel);
                        break;
                        
                    case "BuybackTokenomicModel":
                        dynamic buybackModel = tokenomic;
                        dynamic buybackValidator = Type.GetType("Moongy.RD.Launchpad.Generator.Tokenomics.Buyback.Validators.BuybackTokenomicValidator, Moongy.RD.Launchpad.Generator.Tokenomics.Buyback");
                        buybackValidator.Validate(buybackModel);
                        break;
                        
                    case "ReflectionsTokenomicModel":
                        dynamic reflectionsModel = tokenomic;
                        dynamic reflectionsValidator = Type.GetType("Moongy.RD.Launchpad.Generator.Tokenomics.Reflections.Validators.ReflectionsTokenomicValidator, Moongy.RD.Launchpad.Generator.Tokenomics.Reflections");
                        reflectionsValidator.Validate(reflectionsModel);
                        break;
                        
                    case "AntiWhaleTokenomicModel":
                        dynamic antiWhaleModel = tokenomic;
                        dynamic antiWhaleValidator = Type.GetType("Moongy.RD.Launchpad.Generator.Tokenomics.AntiWhale.Validators.AntiWhaleTokenomicValidator, Moongy.RD.Launchpad.Generator.Tokenomics.AntiWhale");
                        antiWhaleValidator.Validate(antiWhaleModel);
                        break;
                        
                    default:
                        break;
                }
            }
            catch (Exception ex) when (!(ex is InvalidTokenomicException))
            {
                throw new InvalidTokenomicException($"Failed to validate {typeName}: {ex.Message}. Ensure appropriate validator is available.", ex);
            }
            
        }
    }
}