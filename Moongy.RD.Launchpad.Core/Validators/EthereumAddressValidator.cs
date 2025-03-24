using System.Text.RegularExpressions;
using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Core.Models;

namespace Moongy.RD.Launchpad.Core.Validators
{
    public static class EthereumAddressValidator
    {
        public static void Validate(Address? address, bool isRequired = false, string? addressName = "")
        {
            var addressRegex = new Regex(@"^0x[a-fA-F0-9]{40}$", RegexOptions.Compiled);
            if (!isRequired && Address.IsNullOrEmpty(address)) return;
            if (Address.IsNullOrEmpty(address)) throw new AddressIsRequiredException(addressName);
            if (!addressRegex.IsMatch(address!)) throw new InvalidAddressException(addressName, address.ToString());
        }
    }
}
