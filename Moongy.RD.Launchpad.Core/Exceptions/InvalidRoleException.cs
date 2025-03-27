namespace Moongy.RD.Launchpad.Core.Exceptions
{
    public class InvalidRoleException(string roleName, string role) : RoleException(
        $"Role {roleName} has invalid format: {role}. Roles must contain only uppercase letters and underscores.");
}

