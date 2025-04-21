using Moongy.RD.Launchpad.Core.Enums;

namespace Moongy.RD.Launchpad.Core.Models.Metamodel.Base
{
    public class ContractProperty
    {
        public string Name { get; set; } = "";
        public List<Argument> Arguments { get; set; }
        public PropertyType PropertyType { get; set; }
        public DataType DataType { get; set; }
        public VisibilityModifier Visibility { get; set; }
    }
}
