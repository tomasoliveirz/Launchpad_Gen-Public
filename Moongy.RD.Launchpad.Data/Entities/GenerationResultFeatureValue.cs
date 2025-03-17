using System.ComponentModel.DataAnnotations.Schema;
using Moongy.RD.Launchpad.Data.Base;

namespace Moongy.RD.Launchpad.Data.Entities
{
    public class GenerationResultFeatureValue : Entity
    {
        public string? Value { get; set; }

        [ForeignKey(nameof(ContractGenerationResult))]
        public int ContractGenerationResultId { get; set; }
        public virtual ContractGenerationResult? ContractGenerationResult { get; set; }

        [ForeignKey(nameof(FeatureInContractType))]
        public int FeatureInContractTypeId { get; set; }
        public virtual FeatureInContractType? FeatureInContractType { get; set; }

    }
}
