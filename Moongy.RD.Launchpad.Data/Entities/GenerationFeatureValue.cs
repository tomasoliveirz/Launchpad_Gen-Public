using System.ComponentModel.DataAnnotations.Schema;
using Moongy.RD.Launchpad.Data.Base;

namespace Moongy.RD.Launchpad.Data.Entities
{
    public class GenerationFeatureValue : Entity
    {
        [ForeignKey(nameof(FeatureOnContractFeatureGroup))]
        public int FeatureOnContractFeatureGroupId { get; set; }
        public virtual FeatureOnContractFeatureGroup? FeatureOnContractFeatureGroup { get; set; }

        public string? Value { get; set; }


        [ForeignKey(nameof(ContractGenerationResult))]
        public int ContractGenerationResultId { get; set; }
        public virtual ContractGenerationResult? ContractGenerationResult { get; set; }
    }
}
