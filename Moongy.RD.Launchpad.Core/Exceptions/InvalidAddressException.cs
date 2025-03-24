using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.Launchpad.Core.Exceptions
{
    public class InvalidAddressException(string @for, string address) : Exception($"Address {address} is not valid for {@for}")
    {
    }
}
