using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.ERC20Fields
{
    public class TokenAllowancesField
    {
        public FieldDefinition Build()
        {
            return new FieldDefinition
            {
                Name = "_allowances",
                Type = new TypeReference
                {
                    Kind = TypeReferenceKind.Mapping,
                    KeyType = DataTypeReference.Address,
                    ValueType = new TypeReference
                    {
                        Kind = TypeReferenceKind.Mapping,
                        KeyType = DataTypeReference.Address,
                        ValueType = DataTypeReference.Uint256
                    }
                },
                Visibility = Visibility.Private,
            };
        }
    }
}
