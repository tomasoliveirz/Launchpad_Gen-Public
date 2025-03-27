namespace Moongy.RD.Launchpad.Core.Exceptions;

public class RequiredEthereumAddressException(string paramName)
    : TokenValidationException($"{paramName} cannot be null or empty.");