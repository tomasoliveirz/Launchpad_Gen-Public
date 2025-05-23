using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.ERC20Fields
{
    public class TokenSymbolField
    {
        public FieldDefinition Build()
        {
            return new FieldDefinition
            {
                Name = "_symbol",
                Type = DataTypeReference.String,
                Visibility = Visibility.Private,
            };
        }
    }
}
