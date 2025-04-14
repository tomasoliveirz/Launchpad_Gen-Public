namespace Moongy.RD.Launchpad.Core.Exceptions
{
    public class RoleIsRequiredException(string roleName)
        : RoleException($"Role {roleName} is required but was not provided.");
}
