using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Helpers;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Structs;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.ScribanRenderingModels;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Expressions;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Helpers;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Structs;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.ScribanRenderingModels;
using System;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors
{
    public class StructProcessor() : BaseSolidityTemplateProcessor<StructModel>("Struct")
    {
        public override string Render(StructModel model)
        {
            var result = Transform(model);

            return base.Render(result);
        }

        private static StructRenderingModel Transform(StructModel model)
        {
            var result = new StructRenderingModel()
            {
                Name = model.Name,
            };
            if (model.Properties.Count() != model.Properties.DistinctBy(x => x.Name).Count())
                throw new Exceptions.DuplicateException("struct", model.Name, "properties");
            result.Properties = TransfromProperties(model.Properties);
            return result;
        }
        private static string[] TransfromProperties(StructPropertyModel[] properties)
        {
            return [.. properties.Select(TransformProperty)];
        }

        private static string TransformProperty(StructPropertyModel property)
        {
            var dataType = SolidityReferenceTypeSyntaxHelper.RenderTypeReference(property.DataType);
            return $"{dataType} {property.Name};";
        }
    }
}