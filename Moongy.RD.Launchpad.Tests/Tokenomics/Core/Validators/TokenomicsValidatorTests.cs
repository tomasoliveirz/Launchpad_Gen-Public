using System;
using System.Collections.Generic;
using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Generator.Tokenomics.AntiWhale.Models;
using Moongy.RD.Launchpad.Generator.Tokenomics.Buyback.Models;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Enums;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Validators;
using Moongy.RD.Launchpad.Generator.Tokenomics.LiquidityGeneration.Models;
using Moongy.RD.Launchpad.Generator.Tokenomics.Reflections.Models;
using Moongy.RD.Launchpad.Generator.Tokenomics.Tax.Models;
using Xunit;

namespace Moongy.RD.Launchpad.Tests.Tokenomics.Core.Validators
{
    public class TokenomicsValidatorTests
    {
        [Fact]
        public void Validate_WithValidTokenomics_ShouldNotThrowException()
        {
            var tokenomics = new List<ITokenomic>
            {
                new TaxTokenomicModel { TaxPercentage = 5.0, TriggerMode = TokenomicTriggerMode.Automatic },
                new AntiWhaleTokenomicModel { MaxWalletPercentage = 3, TriggerMode = TokenomicTriggerMode.Automatic }
            };

            var exception = Record.Exception(() => TokenomicsValidator.Validate(tokenomics, 20));
            Assert.Null(exception);
        }

        [Fact]
        public void Validate_WithExcessiveTotalTax_ShouldThrowTokenTaxExceededException()
        {
            var tokenomics = new List<ITokenomic>
            {
                new TaxTokenomicModel { TaxPercentage = 12.0 },
                new TaxTokenomicModel { TaxPercentage = 10.0 }
            };

            var exception = Assert.Throws<TokenTaxExceededException>(() => 
                TokenomicsValidator.Validate(tokenomics, 20));
            
            Assert.Contains("22", exception.Message);
            
            // podem ter mais do que dois tokenomics do mesmo tipo??
        }

        [Fact]
        public void ValidateCompatibility_WithIncompatibleCombination_ShouldThrowInvalidTokenomicException()
        {
            var tokenomics = new List<ITokenomic>
            {
                new ReflectionsTokenomicModel { TriggerMode = TokenomicTriggerMode.Automatic },
                new BuybackTokenomicModel { TriggerMode = TokenomicTriggerMode.Automatic },
                new LiquidityGenerationTokenomicModel { TriggerMode = TokenomicTriggerMode.Automatic }
            };

            var exception = Assert.Throws<InvalidTokenomicException>(() => 
                TokenomicsValidator.ValidateCompatibility(tokenomics));
            
            Assert.Contains("exceeds recommended computational limits", exception.Message);
        }

        [Fact]
        public void ValidateWeight_WithExcessiveWeight_ShouldThrowTokenWeightExceededException()
        {
            // mock 
            var tokenomics = new List<ITokenomic>
            {
                new ReflectionsTokenomicModel { TriggerMode = TokenomicTriggerMode.Automatic },
                new BuybackTokenomicModel { TriggerMode = TokenomicTriggerMode.Automatic }
            };

            var exception = Assert.Throws<TokenWeightExceededException>(() => 
                TokenomicsValidator.ValidateWeight(tokenomics));
            
            Assert.Contains("Token weight exceeded", exception.Message);
        }

        [Fact]
        public void CalcManualModeWeight_WithReflectionTokenomic_ShouldReturnReducedWeight()
        {
            var reflectionTokenomic = new ReflectionsTokenomicModel 
            { 
                TriggerMode = TokenomicTriggerMode.Manual 
            };

            var method = typeof(TokenomicsValidator).GetMethod("CalcManualModeWeight", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

            var result = (double)method.Invoke(null, new object[] { reflectionTokenomic });

            Assert.Equal(40.0, result);
        }
    }
}