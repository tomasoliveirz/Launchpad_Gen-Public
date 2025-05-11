using System;
using System.Collections.Generic;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements.Expressions;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements
{
    public class EmitStatement : StatementModel
    {
        public string EventName { get; }
        public List<string> Arguments { get; } = new List<string>();
        
        protected override string TemplateBaseName => "EmitStatement";
        public EmitStatement(string eventName)
        {
            EventName = eventName ?? throw new ArgumentNullException(nameof(eventName));
        }
        
        //methods that add arguments instead of overloaded constructors
        public EmitStatement AddStringArgument(string argument)
        {
            if (argument == null)
                throw new ArgumentNullException(nameof(argument));
                
            Arguments.Add(argument);
            return this;
        }
        
        public EmitStatement AddExpressionArgument(ExpressionModel expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));
                
            Arguments.Add(expression.ToString());
            return this;
        }
        
        public override void ProcessProperties(Dictionary<string, object> properties)
        {
            base.ProcessProperties(properties);
            if (!properties.ContainsKey("EventName"))
            {
                properties["EventName"] = EventName;
            }
        }
    }
}