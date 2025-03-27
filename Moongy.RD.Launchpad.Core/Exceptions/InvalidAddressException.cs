using System;

namespace Moongy.RD.Launchpad.Core.Exceptions
{
    public class InvalidAddressException : Exception
    {
        public string ParamName { get; }
        
        public string Address { get; }

        public InvalidAddressException(string paramName) 
            : base($"Invalid address provided for '{paramName}'")
        {
            ParamName = paramName;
            Address = string.Empty;
        }
    }
}