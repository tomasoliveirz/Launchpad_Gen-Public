using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Events;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Parameters;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.ScribanRenderingModels;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Renderers;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors
{
    public class EventProcessor() : BaseSolidityTemplateProcessor<EventModel>("Event")
    {
        public override string Render(EventModel model)
        {
            var result = Transform(model);
            return base.Render(result);
        }

        private EventRenderingModel Transform(EventModel model)
        {
            var result = new EventRenderingModel() { Name = model.Name};
            result.Parameters = TransformParameters(model.Parameters);
            return result;
        }

        private string[] TransformParameters(IEnumerable<EventParameterModel> parameters)
        {
            return parameters.OrderBy(x => x.Index).Select(TransformParameter).ToArray();
        }

        private string TransformParameter(EventParameterModel parameter)
        {
            var dataType = SolidityReferenceTypeSyntaxRenderer.RenderTypeReference(parameter.Type);
            var indexedExpression = parameter.IsIndexed ? "indexed ":"";
            return $"{dataType} {indexedExpression}{parameter.Name}";
        }
    }
}
