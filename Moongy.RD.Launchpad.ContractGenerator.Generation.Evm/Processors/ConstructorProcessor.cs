using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.ScribanRenderingModels;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors;

public class ConstructorProcessor() : BaseSolidityTemplateProcessor<SolidityContractModel>("ContractHeader")
{
    public override string Render(SolidityContractModel model)
    {
        var renderModel = Transform(model);
        return Render(renderModel);
    }

    private static ConstructorRenderingModel Transform(SolidityContractModel model)
    {
        List<string> arguments = TransformArguments(model);
        List<string> baseConstructors = TransformBaseConstructors(model);
        List<string> assignments = TransformAssignments(model);

        return new() { Arguments = arguments, BaseConstructors = baseConstructors, Assignments = assignments};
    }

    private static List<string> TransformAssignments(SolidityContractModel model)
    {
        throw new NotImplementedException();
    }

    private static List<string> TransformBaseConstructors(SolidityContractModel model)
    {
        throw new NotImplementedException();
    }

    private static List<string> TransformArguments(SolidityContractModel model)
    {
        return null;
    }
}
