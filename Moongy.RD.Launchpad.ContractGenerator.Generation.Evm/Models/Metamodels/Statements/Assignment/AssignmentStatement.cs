using System;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements.Expressions;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements
{
    public class AssignmentStatement : AssignmentStatementBase
    {
        public string Value { get; set; } = string.Empty;
        public ExpressionModel? ValueExpression { get; set; }
        public string Operator { get; set; } = "=";
        
        protected override string TemplateBaseName => "AssignmentStatement";
        
        public AssignmentStatement() { }
        
        // constructor that takes all required parameters
        public AssignmentStatement(string target, string value, string @operator = "=")
        {
            Target = target ?? throw new ArgumentNullException(nameof(target));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Operator = @operator ?? "=";
        }
        
        public AssignmentStatement(ExpressionModel targetExpression, ExpressionModel valueExpression, string @operator = "=")
        {
            TargetExpression = targetExpression ?? throw new ArgumentNullException(nameof(targetExpression));
            ValueExpression = valueExpression ?? throw new ArgumentNullException(nameof(valueExpression));
            Target = targetExpression.ToString();
            Value = valueExpression.ToString();
            Operator = @operator ?? "=";
        }
        
        // half half
        public AssignmentStatement(string target, ExpressionModel valueExpression, string @operator = "=")
        {
            Target = target ?? throw new ArgumentNullException(nameof(target));
            ValueExpression = valueExpression ?? throw new ArgumentNullException(nameof(valueExpression));
            Value = valueExpression.ToString();
            Operator = @operator ?? "=";
        }
        
        public AssignmentStatement(ExpressionModel targetExpression, string value, string @operator = "=")
        {
            TargetExpression = targetExpression ?? throw new ArgumentNullException(nameof(targetExpression));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Operator = @operator ?? "=";
        }
    }
}