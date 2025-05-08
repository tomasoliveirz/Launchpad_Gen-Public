using System.Collections.Generic;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Functions.Body
{
    /// <summary>
    /// Represents information of a statement in the body of a Solidity function.
    /// This class is used as a model for rendering via Scriban template.
    /// </summary>
    public class StatementInfo
    {

        public StatementType Type { get; set; }

        public string? Name { get; set; }
  
        public string? Text { get; set; }
   
        public string? Condition { get; set; }
 
        public string? Message { get; set; }

        public string? Target { get; set; }

        public string? Value { get; set; }

        public string? Operator { get; set; }
        
        public List<string> Arguments { get; set; } = new();
        
        public List<StatementInfo> ThenStatements { get; set; } = new();
        
        public List<StatementInfo> ElseStatements { get; set; } = new();
    }
}