namespace Moongy.RD.Launchpad.Core.Exceptions
{
    public class AddressIsRequiredException(string name) : Exception($"Address for {name} is required.")
    {
    }
}
