using System.Collections.Generic;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements
{
    public abstract class StatementModel
    {
        private const string StatementsTemplateDirectory = "Statements.";
        
        protected abstract string TemplateBaseName { get; }
        
        public virtual string TemplateName => StatementsTemplateDirectory + TemplateBaseName;
        
        public virtual void ProcessProperties(Dictionary<string, object> properties)
        {
        }
    }
}