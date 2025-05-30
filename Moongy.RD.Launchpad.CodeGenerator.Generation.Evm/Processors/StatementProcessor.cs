using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Statements;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Processors
{
    public abstract class StatementProcessor<TStatement> : BaseSolidityTemplateProcessor<TStatement>
        where TStatement : StatementModel
    {
        protected StatementProcessor(string templateName) : base(templateName)
        {
        }
    }
}