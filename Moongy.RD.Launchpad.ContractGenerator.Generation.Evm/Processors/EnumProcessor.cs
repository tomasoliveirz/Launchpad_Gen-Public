using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Helpers;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.ScribanRenderingModels;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors
{
    public class EnumProcessor() : BaseSolidityTemplateProcessor<EnumModel>("Enum")
    {
        public override string Render(EnumModel model)
        {
            var result = Transform(model);
            return base.Render(result);
        }
        private EnumRenderingModel Transform(EnumModel model)
        {
            var result = new EnumRenderingModel() { Name = model.Name };
            result.Values = TransformValues(model.Values);
            return result;
        }

        private string[] TransformValues(IEnumerable<string> values)
        {
            return values.Select(TransformValue).ToArray();
        }

        private string TransformValue(string value)
        {
            return value;
        }
    }
}
