namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Attributes
{
    public class ContractAttribute : Attribute
    {
        public required Type ContractType { get; set; }
    }
}
