using System;
using System.Collections.Generic;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements.Expressions;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements
{
    public class RequireStatement : StatementModel
    {
        public required ExpressionModel Condition { get; init; }
        public string? Message { get; set; }
        
        protected override string TemplateBaseName => "RequireStatement";
        public RequireStatement() { }
        
        public RequireStatement(ExpressionModel condition)
        {
            Condition = condition ?? throw new ArgumentNullException(nameof(condition));
        }
        public RequireStatement(ExpressionModel condition, string message)
        {
            Condition = condition ?? throw new ArgumentNullException(nameof(condition));
            Message = message;
        }
        
        // constructor that takes a string condition and converts it to an expression
        public RequireStatement(string condition)
        {
            Condition = new LiteralExpressionModel(condition);
        }
        
        // constructor that takes a string condition and message
        public RequireStatement(string condition, string message)
        {
            Condition = new LiteralExpressionModel(condition);
            Message = message;
        }
        
        public RequireStatement WithMessage(string message)
        {
            Message = message;
            return this;
        }
        
        public override void ProcessProperties(Dictionary<string, object> properties)
        {
            base.ProcessProperties(properties);
            
            // ensure condition is a string
            if (properties.ContainsKey("Condition") && properties["Condition"] is ExpressionModel condExpr)
            {
                properties["Condition"] = condExpr.ToString();
            }
        }
    }
}