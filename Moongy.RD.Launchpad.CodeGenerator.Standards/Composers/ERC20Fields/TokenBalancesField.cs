using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.ERC20Fields
{
    public class TokenBalancesField
    {
        public FieldDefinition Build()
        {
            return new FieldDefinition
            {
                Name = "_balances",
                Visibility = Visibility.Private,
                Type = new TypeReference
                {
                    Kind = TypeReferenceKind.Mapping,
                    KeyType = DataTypeReference.Address,
                    ValueType = DataTypeReference.Uint256,
                },
            };
        }
    }
}
