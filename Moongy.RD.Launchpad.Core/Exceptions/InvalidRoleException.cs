
namespace Moongy.RD.Launchpad.Core.Exceptions
{
    public class InvalidRoleException(string @for, string role) : Exception($"Role {role} is not valid for {@for}")
    {
    }
}
