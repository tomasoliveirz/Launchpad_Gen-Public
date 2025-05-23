using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.ERC20Fields
{
    public class TokenDecimalsField
    {
        public FieldDefinition Build()
        {
            return new FieldDefinition
            {
                Name = "_decimals",
                Type = DataTypeReference.Uint8,
                Visibility = Visibility.Private,
                Value = "18"
            };
        }
    }
}
