using Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Enums;

namespace Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Models
{
    public class ContractArgument
    {
        public ContractDataType Type { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public int Index { get; set; }
    }
}
