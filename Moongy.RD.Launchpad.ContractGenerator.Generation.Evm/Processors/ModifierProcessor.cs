using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Helpers;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Modifiers;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Parameters;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.ScribanRenderingModels;


namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors
{
    //TODO: Duplicate parameters (RESOLVIDO)
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
            if (model.Parameters.Count != model.Parameters.DistinctBy(x => x.Name).Count())
                throw new Exceptions.DuplicateException("modifier", model.Name, "arguments");
            result.Parameters = TransformParameters(model.Parameters);
            return result;
        }

        private string[] TransformParameters(IEnumerable<ModifierParameterModel> parameters)
        {
            return parameters.OrderBy(x => x.Index).Select(TransformParameter).ToArray();
        }

        private string TransformParameter(ModifierParameterModel parameter)
        {
            var dataType = SolidityReferenceTypeSyntaxHelper.RenderTypeReference(parameter.Type);

            //TODO: Can be replaced with an helper  (RESOLVIDO)
            var location = transformMemoryLocation(parameter.Location);

            return $"{dataType} {location}{parameter.Name}";
        }
        private static string transformMemoryLocation(SolidityMemoryLocation? location)
        {
            return location switch
            {
                SolidityMemoryLocation.None => string.Empty,
                SolidityMemoryLocation.Memory => "memory",
                SolidityMemoryLocation.Storage => "storage",
                SolidityMemoryLocation.Calldata => "calldata",
                _ => throw new ArgumentOutOfRangeException(nameof(location), location, null)
            };
        }
    }
}