using System.Text.RegularExpressions;
using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Core.Models;

namespace Moongy.RD.Launchpad.Core.Validators
{
    public static class EthereumAddressValidator
    {
        private static readonly Regex EthAddressRegex = new("^0x[0-9a-fA-F]{40}$", RegexOptions.Compiled);
        
        public static void Validate(Address? address, bool isRequired = false, string paramName = "address")
        {
            if (!isRequired && Address.IsNullOrEmpty(address)) 
                return;
                
            if (isRequired && Address.IsNullOrEmpty(address)) 
                throw new AddressIsRequiredException(paramName);
                
            string addressString = address!;
            
            if (!EthAddressRegex.IsMatch(addressString)) 
                throw new InvalidAddressException(paramName);
        }
    }
}