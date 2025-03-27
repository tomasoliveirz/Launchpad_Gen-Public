namespace Moongy.RD.Launchpad.Core.Exceptions;

public class InvalidTokenIdException(ulong tokenId)
    : TokenValidationException($"Invalid token ID: {tokenId}. Token IDs must be positive.");