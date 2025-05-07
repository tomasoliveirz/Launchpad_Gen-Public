using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.Launchpad.Business.Exceptions
{
    public class NotFoundException(string name, string value) : Exception($"{name} not found. {(string.IsNullOrEmpty(value) ? "":"Id : "+value)}")
    {
        public string Id { get; set; } = value;
    }
}
