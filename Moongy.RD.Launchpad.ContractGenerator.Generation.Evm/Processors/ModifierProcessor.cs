using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Helpers;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Modifiers;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Parameters;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.ScribanRenderingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors
{
    //TODO: Duplicate parameters
    public class ModifierProcessor() : BaseSolidityTemplateProcessor<ModifierModel>("Modifier")
    {
        public override string Render(ModifierModel model)
        {
            var result = Transform(model);
            return base.Render(result);
        }

        private ModifierRenderingModel Transform(ModifierModel model)
        {
            var result = new ModifierRenderingModel()
            {
                Name = model.Name,
                Body = model.Body
            };
            result.Parameters = TransformParameters(model.Parameters);
            return result;
        }

        private string[] TransformParameters(IEnumerable<ModifierParameterModel> parameters)
        {
            return [.. parameters.OrderBy(x => x.Index).Select(TransformParameter)];
        }

        private string TransformParameter(ModifierParameterModel parameter)
        {
            var dataType = SolidityReferenceTypeSyntaxHelper.RenderTypeReference(parameter.Type);

            //TODO: Can be replaced with an helper
            var location =
                 parameter.Location == Enums.SolidityMemoryLocation.None || parameter.Location == null ? "":
                parameter.Location == Enums.SolidityMemoryLocation.Memory ? "memory " :
                parameter.Location == Enums.SolidityMemoryLocation.Calldata ? "calldata ":
                parameter.Location == Enums.SolidityMemoryLocation.Storage ? "storage ": throw new Exception();

            return $"{dataType} {location}{parameter.Name}";
        }
    }
}