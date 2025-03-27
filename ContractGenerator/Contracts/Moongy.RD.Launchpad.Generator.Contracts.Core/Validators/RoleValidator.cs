using System.Text.RegularExpressions;
using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Validators;

public static class RoleValidator
{
    private static readonly Regex RoleNameRegex = new Regex(@"^[A-Z_]+$", RegexOptions.Compiled);
        
    public static void Validate(Role? role, bool isRequired = false, string roleName = "")
    {
        if (!isRequired && Role.IsNullOrEmpty(role)) return;
            
        if (Role.IsNullOrEmpty(role)) 
            throw new RoleIsRequiredException(roleName);
                
        if (!RoleNameRegex.IsMatch(role!.ToString()!)) 
            throw new InvalidRoleException(roleName, role.ToString()!);
    }
}