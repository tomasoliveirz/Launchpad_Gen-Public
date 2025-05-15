using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Functions;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Parameters;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements.Expressions;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Prebuilds
{
    public static class BurnablePrebuild
    {
        public static NormalFunctionModel BurnFunction = new NormalFunctionModel()
        {
            Name = "burn",
            Visibility = SolidityVisibilityEnum.Public,
            IsVirtual = true,
            Parameters = new List<FunctionParameterModel>
    {
        new FunctionParameterModel
        {
            Name = "value",
            Type = DataTypePreBuild.Uint256,
            Index = 0
        }
    },
            Statements = new List<StatementModel>
    {
        new ExpressionStatementModel(
            new MethodCallExpressionModel(
                "_burn",
               [ new MethodCallExpressionModel("_msgSender", Array.Empty<ExpressionModel>()),
                new LiteralExpressionModel("value")]
            )
        )
    }
        };

        public static NormalFunctionModel BurnFromFunction = new NormalFunctionModel()
        {
            Name = "burnFrom",
            Visibility = SolidityVisibilityEnum.Public,
            IsVirtual = true,
            Parameters = new List<FunctionParameterModel>
    {
        new FunctionParameterModel
        {
            Name = "account",
            Type = DataTypePreBuild.Address,
            Index = 0
        },
        new FunctionParameterModel
        {
            Name = "value",
            Type = DataTypePreBuild.Uint256,
            Index = 1
        }
    },
            Statements = new List<StatementModel>
    {
        new ExpressionStatementModel(
            new MethodCallExpressionModel(
                "_spendAllowance",
                new LiteralExpressionModel("account"),
                new MethodCallExpressionModel("_msgSender", Array.Empty<ExpressionModel>()),
                new LiteralExpressionModel("value")
            )
        ),
        new ExpressionStatementModel(
            new MethodCallExpressionModel(
                "_burn",
                new LiteralExpressionModel("account"),
                new LiteralExpressionModel("value")
            )
        )
    }
        };



    }
}
