using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.Launchpad.Core.Exceptions
{
    public class AddressIsRequiredException(string name) : Exception($"Address for {name} is required.")
    {
    }
}
