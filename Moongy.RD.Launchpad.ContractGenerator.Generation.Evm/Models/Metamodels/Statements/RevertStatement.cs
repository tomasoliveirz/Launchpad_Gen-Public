using System;
using System.Collections.Generic;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements
{
    public class RevertStatement : StatementModel
    {
        public string? Name { get; set; }
        public List<string> Arguments { get; set; } = new List<string>();
        public string? Message { get; set; }
        
        protected override string TemplateBaseName => "RevertStatement";
        
        public RevertStatement(string errorName, params string[] arguments)
        {
            Name = errorName ?? throw new ArgumentNullException(nameof(errorName));
            
            if (arguments != null)
            {
                Arguments.AddRange(arguments);
            }
        }
        
        public RevertStatement(string? message = null)
        {
            Message = message;
        }
        
        public RevertStatement AddArgument(string argument)
        {
            Arguments.Add(argument);
            return this;
        }
    }
}