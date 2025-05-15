using System.Collections.Generic;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements.Expressions;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements
{
    public class ForStatement : LoopStatementBase
    {
        public StatementModel? Initialization { get; set; }
        public string? Condition { get; set; }  
        public ExpressionModel? ConditionExpression { get; set; }
        public StatementModel? Iterator { get; set; }
        
        protected override string TemplateBaseName => "ForStatement";
        
        public ForStatement(StatementModel? initialization = null, string? condition = null, StatementModel? iterator = null)
        {
            Initialization = initialization;
            Condition = condition;
            Iterator = iterator;
        }
        
        public ForStatement(StatementModel? initialization, ExpressionModel conditionExpression, StatementModel? iterator = null)
        {
            Initialization = initialization;
            ConditionExpression = conditionExpression;
            Condition = conditionExpression?.ToString();
            Iterator = iterator;
        }
        
        public override ForStatement AddBodyStatement(StatementModel statement)
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