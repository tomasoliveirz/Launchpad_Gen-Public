using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Renderers.ComplexExpressions;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Renderers.Templates;
using Moongy.RD.Launchpad.Core.Helpers;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Renderers.TemplateRenderers
{
    public class ContractHeaderRenderer : BaseTemplateRenderer<SolidityContractModel>
    {
        public ContractHeaderRenderer() : base("ContractHeader")
        {

        }

        public override string Render(SolidityContractModel model)
        {
            return Render(new { name = model.Name, });
        }
    }
}
