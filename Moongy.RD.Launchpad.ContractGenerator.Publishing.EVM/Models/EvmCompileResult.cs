using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.EVM.Models
{
    public class EvmCompileResult
    {
        public bool Success { get; set; }
        public string Bytecode { get; set; }
        public string Abi { get; set; }
        public string Errors { get; set; }
    }

}
