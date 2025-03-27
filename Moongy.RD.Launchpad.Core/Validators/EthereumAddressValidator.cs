using System.Text.RegularExpressions;
using Moongy.RD.Launchpad.Core.Exceptions;

namespace Moongy.RD.Launchpad.Core.Validators
{
    public static class EthereumAddressValidator
    {
        private static readonly Regex EthAddressRegex = new Regex("^0x[0-9a-fA-F]{40}$", RegexOptions.Compiled);
        
        public static void Validate(string? address, bool isRequired = false, string paramName = "address")
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                if (isRequired)
                    throw new RequiredEthereumAddressException(paramName);
                return;
            }

            if (!EthAddressRegex.IsMatch(address))
                throw new InvalidEthereumAddressException(paramName, address);
        }
    }
}
