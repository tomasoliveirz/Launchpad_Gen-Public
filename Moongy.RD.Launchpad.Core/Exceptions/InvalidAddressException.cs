using System;

namespace Moongy.RD.Launchpad.Core.Exceptions
{
    public class InvalidAddressException(string paramName) : Exception($"Invalid address provided for '{paramName}'")
    {
        public string ParamName { get; } = paramName;

        public string Address { get; } = string.Empty;
    }
}