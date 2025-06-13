using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;

namespace Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Augmenters.Tax.Functions;

public class SetTaxFeeFunction
{
    public FunctionDefinition Build()
    {
        var parameters = BuildParameters();

        var newTaxFeeExpr = new ExpressionDefinition
        {
            Kind = ExpressionKind.Identifier,
            Identifier = "newTaxFee"
        };

        var taxFeeExpr = new ExpressionDefinition
        {
            Kind = ExpressionKind.Identifier,
            Identifier = "taxFee"
        };

        var hundredExpr = new ExpressionDefinition
        {
            Kind = ExpressionKind.Literal,
            LiteralValue = "100"
        };

        var condition = new ExpressionDefinition
        {
            Kind = ExpressionKind.Binary,
            Operator = BinaryOperator.GreaterThan,
            Left = newTaxFeeExpr,
            Right = hundredExpr
        };
        
        // require(newTaxFee <= 100, "Tax fee cannot exceed 100%");
        var requireStatement = new FunctionStatementDefinition
        {
            Kind = FunctionStatementKind.Expression,
            Expression = new ExpressionDefinition
            {
                Kind = ExpressionKind.FunctionCall,
                Callee = new ExpressionDefinition { Identifier = "require" },
                Arguments = new List<ExpressionDefinition>
                {
                    new ExpressionDefinition
                    {
                        Kind = ExpressionKind.Binary,
                        Operator = BinaryOperator.LessOrEqualThan,
                        Left = newTaxFeeExpr,
                        Right = hundredExpr
                    },
                    new ExpressionDefinition
                    {
                        Kind = ExpressionKind.Literal,
                        LiteralValue = "\"Tax fee cannot exceed 100%\""
                    }
                }
            }
        };

        var assignment = new FunctionStatementDefinition
        {
            Kind = FunctionStatementKind.Assignment,
            ParameterAssignment = new AssignmentDefinition
            {
                Left = taxFeeExpr,
                Right = newTaxFeeExpr
            }
        };

        var result = new FunctionDefinition
        {
            Name = "setTaxFee",
            Kind = FunctionKind.Normal,
            Visibility = Visibility.Public,
            Modifiers = new List<ModifierDefinition>
            {
                new ModifierDefinition { Name = "onlyOwner" }
            },
            Parameters = parameters,
            Body = new List<FunctionStatementDefinition>
            {
                requireStatement,
                assignment
            }
        };

        return result;
    }

    private List<ParameterDefinition> BuildParameters() =>
    [
        new ParameterDefinition 
        { 
            Name = "newTaxFee", 
            Type = new TypeReference
            {
                Kind = TypeReferenceKind.Simple,
                Primitive = PrimitiveType.Uint256
            }
        }
    ];
}