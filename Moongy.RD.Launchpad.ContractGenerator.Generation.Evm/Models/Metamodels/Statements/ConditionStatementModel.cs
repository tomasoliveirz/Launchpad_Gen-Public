using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Assigments
{
    public class ConditionStatementModel : StatementModel
    {
        public List<ConditionalStatementBlock> ConditionalStatements { get; set; } = [];
    }

    public class ConditionalStatementBlock
    {
        public ComparisonModel Comparison { get; set; }
        public List<StatementModel> Statements { get; set; } = [];
    }
}
