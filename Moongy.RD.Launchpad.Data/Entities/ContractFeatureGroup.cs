using System.ComponentModel.DataAnnotations.Schema;
using Moongy.RD.Launchpad.Data.Base;

namespace Moongy.RD.Launchpad.Data.Entities;

public class ContractFeatureGroup : EntityWithNameAndDescription
{
    public string? DataType { get; set; }

    [ForeignKey(nameof(ContractGenerationResult))]
    public int ContractGenerationResultId { get; set; }
    public virtual ContractGenerationResult? ContractGenerationResult { get; set; }

    public virtual ICollection<FeatureOnContractFeatureGroup> FeaturesOnContractFeatureGroup { get; set; } = [];
}
