using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Generator
{
    public class TransferFunction
    {
        public FunctionDefinition Build()
        {
            var parameters = BuildParameters();

            var res = new FunctionDefinition
            {
                Name = "_transfer",
                Kind = FunctionKind.Normal,
                Visibility = Visibility.Internal,
                Parameters = parameters,
            };
            return res;
        }

        public List<ParameterDefinition> BuildParameters()
        {
            var from = new ParameterDefinition
            {
                Name = "from",
                Type = DataTypeReference.Address
            };
            var to = new ParameterDefinition
            {
                Name = "to",
                Type = DataTypeReference.Address
            };
            var value = new ParameterDefinition
            {
                Name = "value",
                Type = DataTypeReference.Uint256
            };

            var parameters = new List<ParameterDefinition>
            {
                from,
                to,
                value
            };
            return parameters;
        }
    }
}
