using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Imports;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Parameters;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.State;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Renderers.ComplexExpressions;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Renderers.Templates
{
    public class ContractConstructorRenderer : BaseTemplateRenderer<SolidityContractModel>
    {
        public ContractConstructorRenderer() : base("ContractConstructor")
        {

        }

        public override string Render(SolidityContractModel model)
        {
            var constructorParameters = GetConstructorParameters(model).DistinctBy(x => x.Name);
            var parameterList = SoliditySyntaxRenderer.Parameters.Render(constructorParameters);
            var baseConstructorList = RenderBaseClassConstructors(model.BaseContracts.DistinctBy(x => x.Name));
            var parameters = string.Join(", ", parameterList);
            var baseConstructors = string.Join(" ", baseConstructorList);
            var assignments = RenderAssignments(constructorParameters, model.StateProperties);
            return Render(new { parameters , baseConstructors, assignments });
        }

        private List<string> RenderAssignments(IEnumerable<ConstructorParameterModel> parameters, List<StatePropertyModel> stateProperties)
        {
            var result = new List<string>();
            foreach(var parameter in parameters.Where(x => x.AssignedTo !=null))
            {
                var stateProperty = stateProperties.Where(x => x.Name == parameter.AssignedTo).FirstOrDefault();
                if (stateProperty == null) continue;
                var sourceType = SoliditySyntaxRenderer.ReferenceType.Render(parameter.Type);
                var targetType = SoliditySyntaxRenderer.ReferenceType.Render(stateProperty.Type);
                if (sourceType != targetType) throw new Exception("Constructor type mismatch");
                result.Add($"{stateProperty.Name} = {parameter.Name};");
            }
            return result;
        }

        private List<ConstructorParameterModel> GetConstructorParameters(SolidityContractModel model)
        {
            var abstractionParameters = model.BaseContracts.SelectMany(x => x.ConstructorParameters);
            var result = new List<ConstructorParameterModel>();
            foreach(var param in model.ConstructorParameters)
            {
                if (abstractionParameters.Any(x => x.Name == param.Name)) continue;
                result.Add(param);
            }
            return result;
        }

        private List<string> RenderBaseClassConstructors(IEnumerable<AbstractionImportModel> baseModels)
        {
            var result = new List<string>();
            foreach (var baseModel in baseModels)
            {
                var arguments = SoliditySyntaxRenderer.Parameters.RenderAsValue(baseModel.ConstructorParameters);
                var constructor = $"{baseModel.Name}({arguments})";
                result.Add(constructor);
            }
            return result;
        }

    }
}
