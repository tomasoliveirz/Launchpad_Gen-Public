using System;
using System.Collections.Generic;
using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Core.Models;
using Moongy.RD.Launchpad.Generator.Tokenomics.AntiWhale.Models;
using Moongy.RD.Launchpad.Generator.Tokenomics.AntiWhale.Validators;
using Xunit;

namespace Moongy.RD.Launchpad.Tests.Tokenomics.AntiWhale.Validators
{
    public class AntiWhaleTokenomicValidatorTests
    {
        [Fact]
        public void Validate_WithValidModel_ShouldNotThrowException()
        {
            var model = new AntiWhaleTokenomicModel
            {
                MaxWalletPercentage = 2,
                NotAplicableAddresses = new List<Address>
                {
                    new Address("0x1234567890123456789012345678901234567890")
                }
            };

            var exception = Record.Exception(() => AntiWhaleTokenomicValidator.Validate(model));
            Assert.Null(exception);
        }

        [Fact]
        public void Validate_WithNullModel_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => AntiWhaleTokenomicValidator.Validate(null));
        }

        [Fact]
        public void Validate_WithNegativePercentage_ShouldThrowInvalidTokenomicException()
        {
            var model = new AntiWhaleTokenomicModel
            {
                MaxWalletPercentage = -1
            };

            var exception = Assert.Throws<InvalidTokenomicException>(() => 
                AntiWhaleTokenomicValidator.Validate(model));
            
            Assert.Contains("must be between", exception.Message);
        }

        [Fact]
        public void Validate_WithExcessivePercentage_ShouldThrowInvalidTokenomicException()
        {
            var model = new AntiWhaleTokenomicModel
            {
                MaxWalletPercentage = 101
            };

            var exception = Assert.Throws<InvalidTokenomicException>(() => 
                AntiWhaleTokenomicValidator.Validate(model));
            
            Assert.Contains("must be between", exception.Message);
        }

        [Fact]
        public void Validate_WithNoExcludedAddresses_ShouldLogWarning()
        {
            var model = new AntiWhaleTokenomicModel
            {
                MaxWalletPercentage = 3,
                NotAplicableAddresses = new List<Address>()
            };


            var exception = Record.Exception(() => AntiWhaleTokenomicValidator.Validate(model));
            
            Assert.Null(exception); 
            
            // como implementar a verificação de log?
        }
    }
}