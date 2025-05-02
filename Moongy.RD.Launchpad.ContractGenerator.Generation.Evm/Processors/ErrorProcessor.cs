using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Helpers;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Errors;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Parameters;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.ScribanRenderingModels;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors
{
    public class ErrorProcessor() : BaseSolidityTemplateProcessor<ErrorModel>("Error")
    {
        public override string Render(ErrorModel model)
        {
            var result = Transform(model);
            return base.Render(result);
        }
        private static ErrorRenderingModel Transform(ErrorModel model)
        {
            var result = new ErrorRenderingModel()
            {
                Name = model.Name,
            };
            result.Parameters = TransfromParameters(model.Parameters);
            return result;
        }
        private static string[] TransfromParameters(List<ErrorParameterModel> parameters)
        {
            return parameters.OrderBy(x => x.Index).Select(TransformParameter).ToArray();
        }
        private static string TransformParameter(ErrorParameterModel parameter)
        {
            var dataType = SolidityReferenceTypeSyntaxHelper.RenderTypeReference(parameter.Type);
            return $"{dataType} {parameter.Name}";
        }
    }

   }

