using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Helpers;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.ScribanRenderingModels;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors
{

    //TODO: Avoid duplicate values.
    public class EnumProcessor() : BaseSolidityTemplateProcessor<EnumModel>("Enum")
    {
        public override string Render(EnumModel model)
        {
            var result = Transform(model);
            return base.Render(result);
        }
        private EnumRenderingModel Transform(EnumModel model)
        {
            var result = new EnumRenderingModel() { Name = model.Name, Values = [.. model.Values] };
            return result;
        }

    }
}
