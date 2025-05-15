using System.Collections.Generic;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements.Expressions;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements
{
    public abstract class AssignmentStatementBase : StatementModel
    {
        public required string Target { get; set; } = string.Empty;
        public ExpressionModel? TargetExpression { get; set; }
        
        public override void ProcessProperties(Dictionary<string, object> properties)
        {
            base.ProcessProperties(properties);
            
            // ensure target is a string
            if (properties.ContainsKey("TargetExpression") && properties["TargetExpression"] is ExpressionModel targetExpr)
            {
                properties["TargetExpression"] = targetExpr.ToString();
            }
        }
    }
}