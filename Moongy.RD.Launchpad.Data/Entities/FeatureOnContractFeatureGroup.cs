using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Moongy.RD.Launchpad.Data.Base;

namespace Moongy.RD.Launchpad.Data.Entities;

public class FeatureOnContractFeatureGroup : Entity
{

    [ForeignKey(nameof(ContractFeature))]
    public int ContractFeatureId { get; set; }


    [ForeignKey(nameof(ContractFeatureGroup))]
    public int ContractFeatureGroupId { get; set; }

    public virtual ContractFeature? ContractFeature { get; set; }

    public virtual ContractFeatureGroup? ContractFeatureGroup { get; set; }

    public virtual ICollection<GenerationFeatureValue> ContractGenerationFeatureValues { get; set; } = [];


}
