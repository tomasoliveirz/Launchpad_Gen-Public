using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements
{
    public class ComparisonModel
    {
        public string? LeftSideValue { get; set; }
        public string? RightSideValue { get; set; }

        public ComparisonModel? LeftSideComparison { get; set; }
        public ComparisonModel? RightSideComparison { get; set; }

        public ComparisonOperatorEnum Operator { get; set; }
    }
}
