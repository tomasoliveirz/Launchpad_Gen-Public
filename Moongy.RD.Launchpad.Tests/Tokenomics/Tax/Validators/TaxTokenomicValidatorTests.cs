using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Core.Models;
using Moongy.RD.Launchpad.Generator.Tokenomics.Tax.Models;
using Moongy.RD.Launchpad.Generator.Tokenomics.Tax.Validators;

namespace Moongy.RD.Launchpad.Tests.Tokenomics.Tax.Validators
{
    public class TaxTokenomicValidatorTests
    {
        [Fact]
        public void Validate_WithValidModel_ShouldNotThrowException()
        {
            var model = new TaxTokenomicModel
            {
                TaxPercentage = 5.0,
                TaxRecipients =
                [
                    new TaxRecipient
                    {
                        Address = new Address("0x1234567890123456789012345678901234567890"),
                        Shares = 100
                    }
                ]
            };

            var exception = Record.Exception(() => TaxTokenomicValidator.Validate(model));
            Assert.Null(exception);
        }

        [Fact]
        public void Validate_WithNullModel_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => TaxTokenomicValidator.Validate(null));
        }

        [Fact]
        public void Validate_WithNoRecipients_ShouldThrowInvalidTokenomicException()
        {
            var model = new TaxTokenomicModel
            {
                TaxPercentage = 5.0,
                TaxRecipients = []
            };

            var exception = Assert.Throws<InvalidTokenomicException>(() => 
                TaxTokenomicValidator.Validate(model));
            
            Assert.Contains("at least one TaxRecipient", exception.Message);
        }

        [Fact]
        public void Validate_WithInvalidAddress_ShouldThrowInvalidTokenomicException()
        {
            var model = new TaxTokenomicModel
            {
                TaxPercentage = 5.0,
                TaxRecipients =
                [
                    new() {
                        Address = null,
                        Shares = 100
                    }
                ]
            };

            var exception = Assert.Throws<InvalidTokenomicException>(() => 
                TaxTokenomicValidator.Validate(model));
            
            Assert.Contains("valid address", exception.Message);
        }

        [Fact]
        public void Validate_WithZeroShares_ShouldThrowInvalidTokenomicException()
        {
            var model = new TaxTokenomicModel
            {
                TaxPercentage = 5.0,
                TaxRecipients =
                [
                    new() {
                        Address = new Address("0x1234567890123456789012345678901234567890"),
                        Shares = 0
                    }
                ]
            };

            var exception = Assert.Throws<InvalidTokenomicException>(() => 
                TaxTokenomicValidator.Validate(model));
            
            Assert.Contains("greater than 0", exception.Message);
        }

        [Fact]
        public void Validate_WithTotalSharesExceeding100_ShouldThrowInvalidTokenomicException()
        {
            var model = new TaxTokenomicModel
            {
                TaxPercentage = 5.0,
                TaxRecipients =
                [
                    new() {
                        Address = new Address("0x1234567890123456789012345678901234567890"),
                        Shares = 60
                    },
                    new() {
                        Address = new Address("0x2234567890123456789012345678901234567890"),
                        Shares = 50
                    }
                ]
            };

            var exception = Assert.Throws<InvalidTokenomicException>(() => 
                TaxTokenomicValidator.Validate(model));
            
            Assert.Contains("cannot exceed 100", exception.Message);
        }
    }
}