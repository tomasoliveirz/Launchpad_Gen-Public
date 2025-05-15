using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Errors;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Functions;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Parameters;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.State;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements.Expressions;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Prebuilds
{
    public static class PermitPrebuild
    {

        private static List<ErrorParameterModel> _eRC2612ExpiredSignatureParameters = [new() { Name = "deadline", Type = DataTypePreBuild.Uint256 }];
        public static ErrorModel ERC2612ExpiredSignature => new ErrorModel() { Name = "ERC2612ExpiredSignature", Parameters = _eRC2612ExpiredSignatureParameters };

        private static List<ErrorParameterModel> _eRC2612InvalidSignerParameters = [
            new() { Name = "signer", Type = DataTypePreBuild.Address },
            new() { Name = "owner", Type = DataTypePreBuild.Address }];

        public static ErrorModel ERC2612InvalidSigner => new ErrorModel() { Name = "ERC2612ExpiredSignature", Parameters = _eRC2612InvalidSignerParameters };

        public static ConstructorParameterModel EIP712ConstructorParameter = new ConstructorParameterModel()
        {
            Name = "name",
            Type = DataTypePreBuild.String,
            Location = SolidityMemoryLocation.Memory,
            AssignedTo = null,
            Index = 0
        };

        public static StatePropertyModel PERMIT_HASH = new StatePropertyModel()
        {
            Type = DataTypePreBuild.Bytes32,
            Name = "PERMIT_TYPEHASH",
            Visibility = SolidityVisibilityEnum.Private,
            InitialValue = "keccak256(\"Permit(address owner,address spender,uint256 value,uint256 nonce,uint256 deadline)\")",
            IsConstant = true,
            IsImmutable = false
        };

        private static List<FunctionParameterModel> permitFunctionParameterModels = new List<FunctionParameterModel>()
        {
            new FunctionParameterModel()
            {
                Name = "owner",
                Type = DataTypePreBuild.Address,
                Location = SolidityMemoryLocation.Memory,
                Index = 0
            },
            new FunctionParameterModel()
            {
                Name = "spender",
                Type = DataTypePreBuild.Address,
                Index = 1
            },
            new FunctionParameterModel()
            {
                Name = "value",
                Type = DataTypePreBuild.Uint256,
                Index = 2
            },
            new FunctionParameterModel()
            {
                Name = "deadline",
                Type = DataTypePreBuild.Uint256,
                Index = 3
            },
            new FunctionParameterModel()
            {
                Name = "v",
                Type = DataTypePreBuild.Uint8,
                Index = 4
            },
            new FunctionParameterModel()
            {
                Name = "r",
                Type = DataTypePreBuild.Bytes32,
                Index = 5
            },
            new FunctionParameterModel()
            {
                Name = "s",
                Type = DataTypePreBuild.Bytes32,
                Index = 6
            }
        };

        public static NormalFunctionModel PermitFunction = new NormalFunctionModel()
        {
            Name = "permit",
            Visibility = SolidityVisibilityEnum.Public,
            IsVirtual = true,
            Parameters = permitFunctionParameterModels,
        };

        public static NormalFunctionModel NoncesFunction = new NormalFunctionModel()
        {
            Name = "nonces",
            Visibility = SolidityVisibilityEnum.Public,
            Mutability = SolidityFunctionMutabilityEnum.View,
            IsVirtual = true,
            IsOverride = true,
            OverrideSpecifiers = new List<string> { "IERC20Permit", "Nonces" },
            Parameters = new List<FunctionParameterModel>()
    {
        new FunctionParameterModel()
        {
            Name = "owner",
            Type = DataTypePreBuild.Address,
            Index = 0
        }
    },
            ReturnParameters = new List<ReturnParameterModel>()
    {
        new ReturnParameterModel()
        {
            Name = "",
            Type = DataTypePreBuild.Uint256,
            Index = 0
        }
    },
            Statements = new List<StatementModel>()
{
        new ReturnStatement()
        {
            ValueExpressions = new List<ExpressionModel>
            {
                new MethodCallExpressionModel(
                    "super.nonces",
                    new LiteralExpressionModel("owner")
                )
            }
        }
    }

        };

        public static NormalFunctionModel DomainSeparatorFunction = new NormalFunctionModel()
        {
            Name = "DOMAIN_SEPARATOR",
            Visibility = SolidityVisibilityEnum.External,
            Mutability = SolidityFunctionMutabilityEnum.View,
            IsVirtual = true,
            ReturnParameters = new List<ReturnParameterModel>()
            {
                new ReturnParameterModel()
                {
                    Name = "",
                    Type = DataTypePreBuild.Bytes32,
                    Index = 0
                }
            },
            Statements = new List<StatementModel>()
            {
                new ReturnStatement()
                {
                    ValueExpressions = new List<ExpressionModel>
                    {
                        new MethodCallExpressionModel(
                            "_domainSeparatorV4",
                            new LiteralExpressionModel("")
                        )
                    }
                }
            }
        };
    }
}
