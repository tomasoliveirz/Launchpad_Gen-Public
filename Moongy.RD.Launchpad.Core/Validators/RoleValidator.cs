using System.Text.RegularExpressions;
using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Core.Models;

namespace Moongy.RD.Launchpad.Core.Validators
{
    public static class RoleValidator
    {
        public static void Validate(Role role, bool isRequired = false, string roleName = "")
        {
            var roleRegex = new Regex(@"^[A-Z_]+$", RegexOptions.Compiled);
            if (!isRequired && Role.IsNullOrEmpty(role)) return;
            if (Role.IsNullOrEmpty(role)) throw new RoleIsRequiredException(roleName);
            if (!roleRegex.IsMatch(role!)) throw new InvalidRoleException(roleName, role.ToString());
        }
    }
}
