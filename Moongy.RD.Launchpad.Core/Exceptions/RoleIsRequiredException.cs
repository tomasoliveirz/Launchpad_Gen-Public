namespace Moongy.RD.Launchpad.Core.Exceptions
{
    public class RoleIsRequiredException(string name) : Exception($"Role for {name} is required.")
    {
    }
}
