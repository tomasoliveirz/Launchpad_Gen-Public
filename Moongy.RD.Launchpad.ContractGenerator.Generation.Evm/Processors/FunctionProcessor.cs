using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Exceptions;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Helpers;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Functions;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Parameters;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.ScribanRenderingModels;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors
{
    public class FunctionProcessor() : BaseSolidityTemplateProcessor<FunctionModel> ("Function")
    {
       public override string Render(FunctionModel model)
        {
            var result = Transform(model);
            return base.Render(result);
        }

        private static FunctionRenderingModel Transform(FunctionModel model)
        {
            if (model.IsConstructor) return null; 


            var result = new FunctionRenderingModel
            {
                Name = model.Name,
                Parameters = TransformParameters(model.Parameters),
                Body = model.Body,
                Visibility = TransformVisibility(model.Visibility),
            };
            return result;
        }

        private static string TransformVisibility(SolidityVisibilityEnum visibility)
        {
            return visibility switch
            {
                SolidityVisibilityEnum.Public => "public",
                SolidityVisibilityEnum.Private => "private",
                SolidityVisibilityEnum.Internal => "internal",
                _ => "public",
            };
        }

        private static string[] TransformParameters(List<FunctionParameterModel> parameters)
        {
            return [.. parameters.Select(TransformParameter)];
        }

        public static string TransformParameter(FunctionParameterModel parameter)
        {
            var dataType = SolidityReferenceTypeSyntaxHelper.RenderTypeReference(parameter.Type);
            return $"{dataType} {parameter.Name}";
        }
    }

}
