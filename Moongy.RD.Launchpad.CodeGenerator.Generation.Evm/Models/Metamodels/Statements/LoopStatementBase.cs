using System.Collections.Generic;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Statements
{
    public abstract class LoopStatementBase : StatementModel
    {
        public List<StatementModel> Body { get; set; } = new List<StatementModel>();
        
        public virtual LoopStatementBase AddBodyStatement(StatementModel statement)
        {
            Body.Add(statement);
            return this;
        }
    }
}