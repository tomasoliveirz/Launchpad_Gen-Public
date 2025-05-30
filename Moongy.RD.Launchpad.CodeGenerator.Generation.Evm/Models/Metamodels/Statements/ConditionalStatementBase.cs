using System.Collections.Generic;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Statements
{
    public abstract class ConditionalStatementBase : StatementModel
    {
        public string Condition { get; set; }
        public ExpressionModel? ConditionExpression { get; set; }
        
        public override void ProcessProperties(Dictionary<string, object> properties)
        {
            base.ProcessProperties(properties);
            
            // the condition needs to be a string
            if (properties.ContainsKey("ConditionExpression") && properties["ConditionExpression"] is ExpressionModel condExpr)
            {
                properties["ConditionExpression"] = condExpr.ToString();
            }
        }
    }
}