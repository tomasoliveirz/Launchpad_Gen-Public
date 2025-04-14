namespace Moongy.RD.Launchpad.Core.Exceptions;

public class InvalidEnumException(string enumType, object value)
    : TokenValidationException($"Invalid {enumType} value: {value}");