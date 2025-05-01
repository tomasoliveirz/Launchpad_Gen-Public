using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Expressions;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Base;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Renderers;

public abstract class BaseSoliditySyntaxRenderer<TModel> where TModel : SolidityModel
{
    protected static string WrapParameters(string[] arguments, string? separator = "")
    {
        var joinedArguments = string.Join((separator + " "), arguments);
        return $"{SoliditySymbols.OpenParams}{joinedArguments}{SoliditySymbols.CloseParams}";
    }

    public abstract string Render(TModel model);
}
