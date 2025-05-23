
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.ERC20Fields
{
    public class TokenTotalSupplyField
    {
        public FieldDefinition Build()
        {
            return new FieldDefinition() { 
                Name = "_totalSupply",
                Type = DataTypeReference.Uint256,
                Visibility = Visibility.Private,
            };
        }
    }
}
