using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.ERC20Fields
{
    public class TokenNameField
    {
        public FieldDefinition Build()
        {
            return new FieldDefinition
            {
                Name = "_name",
                Type = DataTypeReference.String,
                Visibility = Visibility.Private,
            };
        }
    }
}
