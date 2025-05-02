using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Helpers;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Events;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Parameters;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.ScribanRenderingModels;

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
            if (model.Parameters.Count != model.Parameters.DistinctBy(x => x.Name).Count())
                throw new Exceptions.DuplicateException("event", model.Name, "arguments");
            result.Parameters = TransformParameters(model.Parameters);
            return result;
        }

        private string[] TransformParameters(IEnumerable<EventParameterModel> parameters)
        {
            return parameters.OrderBy(x => x.Index).Select(TransformParameter).ToArray();
        }

        private string TransformParameter(EventParameterModel parameter)
        {
            var dataType = SolidityReferenceTypeSyntaxHelper.RenderTypeReference(parameter.Type);
            var indexedExpression = parameter.IsIndexed ? "indexed ":"";
            return $"{dataType} {indexedExpression}{parameter.Name}";
        }
    }
}
