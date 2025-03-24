using System.Data;
using System.Net;

namespace Moongy.RD.Launchpad.Core.Models
{
    public class Role
    {
        private readonly string _role;

        public Role(string role)
        {
            _role = role;
        }

        public override string ToString()
        {
            return _role;
        }

        public static bool IsNullOrEmpty(Role? role)
        {
            return role == null || string.IsNullOrEmpty(role.ToString());
        }

        public static implicit operator string(Role role)
        {
            return role?._role ?? "";
        }
    }
}
