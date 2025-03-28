using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Core.Validators;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Enumerables;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Validators
{
    public static class AccessValidator
    {
        public static void Validate(TokenAccess? access, bool isRequired = false)
        {
            
            if (!isRequired && (access == null || access.Type == AccessEnum.NONE)) 
                return;
                
            if (access == null || access.Type == AccessEnum.NONE) 
                throw new ContractRequiresAccessException();
                
            if (access.Type == AccessEnum.OWNABLE) 
                EthereumAddressValidator.Validate(access.Owner, true, "owner");
                
            if (access.Type == AccessEnum.MANAGED) 
            {
                if (access.Managers.Count == 0) 
                    throw new ContractRequiresManagersException();
                    
                foreach(var manager in access.Managers)
                {
                    EthereumAddressValidator.Validate(manager, true, "manager");
                }
            }
            
            if (access.Type == AccessEnum.ROLE) 
            {
                if (access.Roles.Count == 0) 
                    throw new ContractRequiresRolesException();
                    
                foreach (var role in access.Roles)
                {
                    RoleValidator.Validate(role, true, "role");
                }
            }
        }
    }
}
