using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Helpers;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Errors;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Parameters;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.ScribanRenderingModels;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Helpers;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Errors;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Parameters;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.ScribanRenderingModels;
using System;

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
            if (model.Parameters.Count != model.Parameters.DistinctBy(x => x.Name).Count())
                throw new Exceptions.DuplicateException("error", model.Name, "arguments");
            result.Parameters = TransfromParameters(model.Parameters);
            return result;
        }
        private static string[] TransfromParameters(List<ErrorParameterModel> parameters)
        {
            return [.. parameters.OrderBy(x => x.Index).Select(TransformParameter)];
        }
        private static string TransformParameter(ErrorParameterModel parameter)
        {
            var dataType = SolidityReferenceTypeSyntaxHelper.RenderTypeReference(parameter.Type);
            return $"{dataType} {parameter.Name}";
        }
    }

}
