using Moongy.RD.Launchpad.Core.Enums;

namespace Moongy.RD.Launchpad.Core.Models.Metamodel.Base
{
    public abstract class ContractProperty
    {
        public string Name { get; set; } = "";
        public List<Argument> Arguments { get; set; }
        public PropertyType PropertyType { get; set; }
        public string DataType { get; set; } = "";
        public string Visibility { get; set; } = "";
        
    }
}
