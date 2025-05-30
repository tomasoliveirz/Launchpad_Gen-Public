using System.Collections.Generic;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Statements
{
    public abstract class AssignmentStatementBase : StatementModel
    {
        public string Target { get; set; } = string.Empty;
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