namespace Moongy.RD.Launchpad.Core.Models.Metamodel.Base
{
    public abstract class ContractProperty<T>
    {
        public string Name { get; set; } = "";
        public List<T> Arguments { get; set; }
    }
}
