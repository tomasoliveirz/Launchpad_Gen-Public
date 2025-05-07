using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Core.Models;
using Moongy.RD.Launchpad.Generator.Tokenomics.AntiWhale.Models;
using Moongy.RD.Launchpad.Generator.Tokenomics.Buyback.Models;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Enums;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Validators;
using Moongy.RD.Launchpad.Generator.Tokenomics.Deflation.Models;
using Moongy.RD.Launchpad.Generator.Tokenomics.LiquidityGeneration.Models;
using Moongy.RD.Launchpad.Generator.Tokenomics.Reflections.Models;
using Moongy.RD.Launchpad.Generator.Tokenomics.Tax.Models;

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

            var result = (double)method.Invoke(null, [reflectionTokenomic]);

            Assert.Equal(40.0, result);
        }
        
        [Fact]
        public void Validate_WithValidDeflationTokenomic_ShouldNotThrowException()
        {
            var tokenomics = new List<ITokenomic>
            {
                new DeflationTokenomicModel { TaxPercentage = 2.0, TriggerMode = TokenomicTriggerMode.Automatic }
            };

            var exception = Record.Exception(() => TokenomicsValidator.Validate(tokenomics, 20));
            Assert.Null(exception);
        }

        [Fact]
        public void Validate_WithValidLiquidityGenerationTokenomic_ShouldNotThrowException()
        {
            var tokenomics = new List<ITokenomic>
            {
                new LiquidityGenerationTokenomicModel { 
                    TaxPercentage = 2.0, 
                    TriggerMode = TokenomicTriggerMode.Automatic,
                    TaxCollector = new Address("0x1234567890123456789012345678901234567890")
                }
            };

            var exception = Record.Exception(() => TokenomicsValidator.Validate(tokenomics, 20));
            Assert.Null(exception);
        }

        [Fact]
        public void Validate_WithLiquidityGenerationHighTaxPercentage_ShouldThrowInvalidTokenomicException()
        {
            var tokenomics = new List<ITokenomic>
            {
                new LiquidityGenerationTokenomicModel { 
                    TaxPercentage = 5.0, 
                    TriggerMode = TokenomicTriggerMode.Automatic,
                    TaxCollector = new Address("0x1234567890123456789012345678901234567890")
                }
            };

            var exception = Assert.Throws<InvalidTokenomicException>(() => 
                TokenomicsValidator.ValidateIndividualTokenomic(tokenomics[0]));
            
            Assert.Contains("Manual Mode", exception.Message);
        }

        [Fact]
        public void Validate_WithReflectionsHighTaxPercentage_ShouldThrowInvalidTokenomicException()
        {
            var tokenomics = new List<ITokenomic>
            {
                new ReflectionsTokenomicModel { 
                    TaxPercentage = 8.0, 
                    TriggerMode = TokenomicTriggerMode.Automatic
                }
            };

            var exception = Assert.Throws<InvalidTokenomicException>(() => 
                TokenomicsValidator.ValidateIndividualTokenomic(tokenomics[0]));
            
            Assert.Contains("Manual Mode", exception.Message);
        }

        [Fact]
        public void Validate_WithComplexValidCombination_ShouldNotThrowException()
        {
            var tokenomics = new List<ITokenomic>
            {
                new TaxTokenomicModel
                {
                    TaxPercentage = 3.0,
                    TriggerMode = TokenomicTriggerMode.Automatic,
                    TaxRecipients =
                    [
                        new() {
                            Address = new Address("0x1234567890123456789012345678901234567890"),
                            Shares = 100
                        }
                    ]
                },
                new DeflationTokenomicModel { TaxPercentage = 1.0, TriggerMode = TokenomicTriggerMode.Automatic },
                new AntiWhaleTokenomicModel 
                { 
                    MaxWalletPercentage = 2, 
                    NotAplicableAddresses =
                    [
                        new("0x1234567890123456789012345678901234567890")
                    ]
                }
            };

            var exception = Record.Exception(() => TokenomicsValidator.Validate(tokenomics, 20));
            Assert.Null(exception);
        }

        [Fact]
        public void Validate_WithMultipleSameTypeTokenomics_ShouldAddUpTaxes()
        {
            var tokenomics = new List<ITokenomic>
            {
                new TaxTokenomicModel 
                { 
                    TaxPercentage = 8.0,
                    TaxRecipients =
                    [
                        new TaxRecipient
                        {
                            Address = new Address("0x1234567890123456789012345678901234567890"),
                            Shares = 100
                        }
                    ]
                },
                new TaxTokenomicModel 
                { 
                    TaxPercentage = 7.0,
                    TaxRecipients =
                    [
                        new TaxRecipient
                        {
                            Address = new Address("0x2234567890123456789012345678901234567890"),
                            Shares = 100
                        }
                    ]
                },
                new TaxTokenomicModel 
                { 
                    TaxPercentage = 6.0,
                    TaxRecipients =
                    [
                        new TaxRecipient
                        {
                            Address = new Address("0x3234567890123456789012345678901234567890"),
                            Shares = 100
                        }
                    ]
                }
            };

            var exception = Assert.Throws<TokenTaxExceededException>(() => 
                TokenomicsValidator.Validate(tokenomics, 20));
            
            Assert.Contains("21", exception.Message);
        }

        [Fact]
        public void Validate_WithMixedModesTokenomics_ShouldNotThrowException()
        {
            var tokenomics = new List<ITokenomic>
            {
                new ReflectionsTokenomicModel { TaxPercentage = 2.0, TriggerMode = TokenomicTriggerMode.Manual },
                new BuybackTokenomicModel { TaxPercentage = 2.0, TriggerMode = TokenomicTriggerMode.Manual },
                new LiquidityGenerationTokenomicModel 
                { 
                    TaxPercentage = 2.0, 
                    TriggerMode = TokenomicTriggerMode.Automatic,
                    TaxCollector = new Address("0x1234567890123456789012345678901234567890")
                }
            };
            
            var exception = Record.Exception(() => TokenomicsValidator.Validate(tokenomics, 20));
            Assert.Null(exception);
        }

        [Fact]
        public void ValidateCompatibility_WithTooManyAutomaticTokenomics_ShouldThrowInvalidTokenomicException()
        {
            var tokenomics = new List<ITokenomic>
            {
                new ReflectionsTokenomicModel { TriggerMode = TokenomicTriggerMode.Automatic },
                new TaxTokenomicModel 
                { 
                    TaxPercentage = 1.0, 
                    TriggerMode = TokenomicTriggerMode.Automatic,
                    TaxRecipients =
                    [
                        new TaxRecipient
                        {
                            Address = new Address("0x1234567890123456789012345678901234567890"),
                            Shares = 100
                        }
                    ]
                },
                new DeflationTokenomicModel { TaxPercentage = 1.0, TriggerMode = TokenomicTriggerMode.Automatic },
                new AntiWhaleTokenomicModel 
                { 
                    MaxWalletPercentage = 2, 
                    TriggerMode = TokenomicTriggerMode.Automatic,
                    NotAplicableAddresses =
                    [
                        new Address("0x1234567890123456789012345678901234567890")
                    ]
                }
            };

            var exception = Assert.Throws<InvalidTokenomicException>(() => 
                TokenomicsValidator.ValidateCompatibility(tokenomics));
            
            Assert.Contains("Reflections in automatic mode", exception.Message);
        }

        [Fact]
        public void ValidateTax_WithMaximumTaxLimit_ShouldNotThrowException()
        {
            var tokenomics = new List<ITokenomic>
            {
                new TaxTokenomicModel { TaxPercentage = 10.0 },
                new DeflationTokenomicModel { TaxPercentage = 10.0 }
            };

            var exception = Record.Exception(() => TokenomicsValidator.ValidateTax(tokenomics, 20));
            Assert.Null(exception);
        }

        [Fact]
        public void ValidateTax_WithCustomLimit_ShouldRespectTheLimit()
        {
            var tokenomics = new List<ITokenomic>
            {
                new TaxTokenomicModel { TaxPercentage = 8.0 },
                new DeflationTokenomicModel { TaxPercentage = 7.0 }
            };

            var exception = Assert.Throws<TokenTaxExceededException>(() => 
                TokenomicsValidator.ValidateTax(tokenomics, 10));
            
            Assert.Contains("15", exception.Message);
        }
    }
}