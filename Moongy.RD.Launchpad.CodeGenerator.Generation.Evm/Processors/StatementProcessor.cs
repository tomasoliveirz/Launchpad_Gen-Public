using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Statements;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors
{
    public abstract class StatementProcessor<TStatement> : BaseSolidityTemplateProcessor<TStatement>
        where TStatement : StatementModel
    {
        protected StatementProcessor(string templateName) : base(templateName)
        {
        }
    }
}