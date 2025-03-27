namespace Moongy.RD.Launchpad.Core.Exceptions
{
    public class ContractRequiresRolesException()
        : AccessControlException("Contract with ROLE access requires at least one role.");
}
