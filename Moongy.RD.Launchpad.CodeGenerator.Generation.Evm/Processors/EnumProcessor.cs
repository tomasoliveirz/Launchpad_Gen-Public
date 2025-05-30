using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.ScribanRenderingModels;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Helpers;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.ScribanRenderingModels;
using System;

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
            var result = new EnumRenderingModel() { Name = model.Name, Values = model.Values.ToArray() };
            if (model.Values.Count != model.Values.DistinctBy(x => x).Count())
                throw new Exceptions.DuplicateException("enum", model.Name, "values");
            return result;
        }

    }
}