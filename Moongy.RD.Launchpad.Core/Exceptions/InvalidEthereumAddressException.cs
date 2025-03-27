namespace Moongy.RD.Launchpad.Core.Exceptions;

public class InvalidEthereumAddressException(string paramName, string address) : TokenValidationException(
    $"Invalid Ethereum {paramName}: {address}. Must be a valid Ethereum address (0x followed by 40 hexadecimal characters).");