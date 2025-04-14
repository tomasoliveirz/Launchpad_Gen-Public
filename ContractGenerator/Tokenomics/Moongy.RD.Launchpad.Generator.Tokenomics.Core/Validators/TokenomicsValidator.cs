using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
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
        
        // map do nome do tokenomic para o namespace e assembly do validador
        private static readonly Dictionary<string, (string Namespace, string Assembly)> ValidatorInfo = new()
        {
            { "TaxTokenomicModel", ("Moongy.RD.Launchpad.Generator.Tokenomics.Tax.Validators.TaxTokenomicValidator", "Moongy.RD.Launchpad.Generator.Tokenomics.Tax") },
            { "DeflationTokenomicModel", ("Moongy.RD.Launchpad.Generator.Tokenomics.Deflation.Validators.DeflationTokenomicValidator", "Moongy.RD.Launchpad.Generator.Tokenomics.Deflation") },
            { "LiquidityGenerationTokenomicModel", ("Moongy.RD.Launchpad.Generator.Tokenomics.LiquidityGeneration.Validators.LiquidityGenerationTokenomicValidator", "Moongy.RD.Launchpad.Generator.Tokenomics.LiquidityGeneration") },
            { "BuybackTokenomicModel", ("Moongy.RD.Launchpad.Generator.Tokenomics.Buyback.Validators.BuybackTokenomicValidator", "Moongy.RD.Launchpad.Generator.Tokenomics.Buyback") },
            { "ReflectionsTokenomicModel", ("Moongy.RD.Launchpad.Generator.Tokenomics.Reflections.Validators.ReflectionsTokenomicValidator", "Moongy.RD.Launchpad.Generator.Tokenomics.Reflections") },
            { "AntiWhaleTokenomicModel", ("Moongy.RD.Launchpad.Generator.Tokenomics.AntiWhale.Validators.AntiWhaleTokenomicValidator", "Moongy.RD.Launchpad.Generator.Tokenomics.AntiWhale") }
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
            
            // aqui temos que verificar o modo de trigger tambem, como verificar?? caso seja manual mode até poderá ser viável
            if (hasReflection && hasBuyback && hasLiquidity)
                throw new InvalidTokenomicException("Using Reflections, Buyback, and Liquidity together exceeds recommended computational limits.");
            
            if (hasReflection && tokenomics.Count > 3 && tokenomics.Any(t => t.TriggerMode == TokenomicTriggerMode.Automatic))
                throw new InvalidTokenomicException("Reflections in automatic mode with more than 3 tokenomics may exceed computational limits. Consider using manual mode for Reflections.");
        }
        
        private static double CalcManualModeWeight(ITokenomic tokenomic)
        {
            string tokenomicTypeName = tokenomic.GetType().Name;
    
            var attr = tokenomic.GetType().GetCustomAttribute<TokenomicAttribute>();
            double baseWeight = attr?.Weight ?? 0;
    
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
                if (!ValidatorInfo.TryGetValue(typeName, out var validatorData))
                {
                    throw new InvalidTokenomicException(
                        $"No validator configuration found for tokenomic type: {typeName}");
                }
                
                Type validatorType = null;
                string fullTypePath = $"{validatorData.Namespace}, {validatorData.Assembly}";
                validatorType = Type.GetType(fullTypePath);
                
                if (validatorType == null)
                {
                    foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        if (assembly.GetName().Name == validatorData.Assembly)
                        {
                            validatorType = assembly.GetType(validatorData.Namespace);
                            break;
                        }
                    }
                }
                
                if (validatorType == null)
                {
                    string shortTypeName = validatorData.Namespace.Split('.').Last();
                    foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        foreach (var type in assembly.GetTypes())
                        {
                            if (type.Name == shortTypeName)
                            {
                                validatorType = type;
                                break;
                            }
                        }
                        if (validatorType != null)
                            break;
                    }
                }
                
                if (validatorType == null)
                {
                    throw new InvalidTokenomicException(
                        $"Could not find validator type for {typeName}. Validator: {validatorData.Namespace}");
                }
                
                MethodInfo validateMethod = validatorType.GetMethod("Validate");
                if (validateMethod == null)
                {
                    throw new InvalidTokenomicException(
                        $"Validator for {typeName} does not have a Validate method.");
                }
                
                validateMethod.Invoke(null, new object[] { tokenomic });
            }
            catch (TargetInvocationException ex) when (ex.InnerException is InvalidTokenomicException)
            {
                throw ex.InnerException;
            }
            catch (Exception ex) when (!(ex is InvalidTokenomicException))
            {
                throw new InvalidTokenomicException(
                    $"Failed to validate {typeName}: {ex.Message}. Ensure appropriate validator is available.", ex);
            }
        }
    }
}