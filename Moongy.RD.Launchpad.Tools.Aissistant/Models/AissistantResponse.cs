using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.Launchpad.Tools.Aissistant.Models
{
    public class AissistantResponse
    {
        public bool Success { get; set; }
        public string Content { get; set; }
        public string? RawApiResponse { get; set; }
    }
}
