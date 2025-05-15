using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Expressions;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Helpers;

public abstract class BaseSoliditySyntaxHelper<TModel> where TModel : SolidityModel
{
    protected static string WrapParameters(string[] arguments, string? separator = "")
    {
        var joinedArguments = string.Join(separator + " ", arguments);
        return $"{SoliditySymbols.OpenParams}{joinedArguments}{SoliditySymbols.CloseParams}";
    }

    public abstract string Render(TModel model);
}
