using System;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements.Expressions;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements
{
    public class DoWhileStatement : LoopStatementBase
    {
        public string? Condition { get; set; }  // for template use
        public ExpressionModel? ConditionExpression { get; set; }
        
        protected override string TemplateBaseName => "DoWhileStatement";
        
        // string condition
        public DoWhileStatement(string condition)
        {
            Condition = condition ?? throw new ArgumentNullException(nameof(condition));
        }
        
        // ExpressionModel condition
        public DoWhileStatement(ExpressionModel conditionExpression)
        {
            ConditionExpression = conditionExpression ?? throw new ArgumentNullException(nameof(conditionExpression));
            Condition = conditionExpression.ToString();
        }
        
        public override DoWhileStatement AddBodyStatement(StatementModel statement)
        {
            Body.Add(statement);
            return this;
        }
        
        public override void ProcessProperties(Dictionary<string, object> properties)
        {
            base.ProcessProperties(properties);
            
            // convert ConditionExpression to string if present
            if (properties.ContainsKey("ConditionExpression") && properties["ConditionExpression"] is ExpressionModel condExpr)
            {
                properties["Condition"] = condExpr.ToString();
            }
        }
    }
}