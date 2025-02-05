using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.Launchpad.Business.Exceptions
{
    public class InvalidModelException(string reason) : Exception($"The model is not valid: {reason}")
    {
    }
}
