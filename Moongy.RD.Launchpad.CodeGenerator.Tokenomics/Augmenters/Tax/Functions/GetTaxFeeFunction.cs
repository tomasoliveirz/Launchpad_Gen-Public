using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;

namespace Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Augmenters.Tax.Functions;

public class GetTaxFeeFunction
{
    public FunctionDefinition Build()
    {
        var taxFeeExpr = new ExpressionDefinition
        {
            Kind = ExpressionKind.Identifier,
            Identifier = "taxFee"
        };

        var returnStatement = new FunctionStatementDefinition
        {
            Kind = FunctionStatementKind.Return,
            ReturnValues = new List<ExpressionDefinition> { taxFeeExpr }
        };

        var result = new FunctionDefinition
        {
            Name = "getTaxFee",
            Visibility = Visibility.Public,
            ReturnParameters = new List<ParameterDefinition>
            {
            },
            Body = new List<FunctionStatementDefinition>
            {
                returnStatement
            }
        };

        return result;
    }
}