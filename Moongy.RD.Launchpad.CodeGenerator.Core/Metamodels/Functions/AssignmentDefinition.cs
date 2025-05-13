using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions
{
    public class AssignmentDefinition
    {
        public ExpressionDefinition Left { get; set; } = default!;
        public ExpressionDefinition Right { get; set; } = default!;
    }
}
