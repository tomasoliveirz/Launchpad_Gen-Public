using Moongy.RD.Launchpad.Core.Exceptions;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Validators;

public static class NameValidator
{
    public static void Validate(string? name, bool isRequired = false)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            if (isRequired)
                throw new RequiredTokenNameException();
            return;
        }

        if (name.Length < 1 || name.Length > 50)
            throw new InvalidTokenNameException(name);
    }
}