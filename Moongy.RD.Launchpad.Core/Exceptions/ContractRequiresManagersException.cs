namespace Moongy.RD.Launchpad.Core.Exceptions
{
    public class ContractRequiresManagersException()
        : AccessControlException("Contract with MANAGED access requires at least one manager.");
}
