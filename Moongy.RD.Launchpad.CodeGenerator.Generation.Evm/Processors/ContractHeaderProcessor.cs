using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.ScribanRenderingModels;


namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Processors;
public class ContractHeaderProcessor() : BaseSolidityTemplateProcessor<SolidityContractModel>("ContractHeader")
{
    public override string Render(SolidityContractModel model)
    {
        var renderModel = Transform(model);
        return Render(renderModel);
    }

    private static ContractHeaderRenderingModel Transform(SolidityContractModel model)
    {
        var dependencies = TransformDependencies(model);
        return new() { Dependencies = TransformDependencies(model), Name = model.Name };
    }

    private static IEnumerable<string> TransformDependencies(SolidityContractModel model)
    {
        var dependencies = new List<string>();
        foreach (var abstraction in model.BaseContracts)
        {
            dependencies.Add(abstraction.Name);
        }
        foreach (var @interface in model.Interfaces)
        {
            dependencies.Add(@interface.Name);
        }
        return dependencies.Distinct();
    }
}