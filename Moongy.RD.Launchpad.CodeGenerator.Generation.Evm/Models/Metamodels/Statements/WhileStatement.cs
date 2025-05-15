using System;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Statements
{
    public class WhileStatement : LoopStatementBase
    {
        public string? Condition { get; set; }  // For template use
        public ExpressionModel? ConditionExpression { get; set; }
        
        protected override string TemplateBaseName => "WhileStatement";
        
        public WhileStatement(string condition)
        {
            Condition = condition ?? throw new ArgumentNullException(nameof(condition));
        }
        public WhileStatement(ExpressionModel conditionExpression)
        {
            ConditionExpression = conditionExpression ?? throw new ArgumentNullException(nameof(conditionExpression));
            Condition = conditionExpression.ToString();
        }
        
        public override WhileStatement AddBodyStatement(StatementModel statement)
        {
            Body.Add(statement);
            return this;
        }
        
        public override void ProcessProperties(Dictionary<string, object> properties)
        {
            base.ProcessProperties(properties);
            
            if (properties.ContainsKey("ConditionExpression") && properties["ConditionExpression"] is ExpressionModel condExpr)
            {
                properties["Condition"] = condExpr.ToString();
            }
        }
    }
}