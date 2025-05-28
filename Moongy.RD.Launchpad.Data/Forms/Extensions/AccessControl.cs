using System.Security.AccessControl;

namespace Moongy.RD.Launchpad.Data.Forms.Extensions;
public class AccessControl
{
    AccessControlType Type { get; set; }
    public List<string> Roles { get; set; } = [];
}
