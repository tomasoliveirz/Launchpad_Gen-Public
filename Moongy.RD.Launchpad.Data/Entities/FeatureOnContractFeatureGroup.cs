using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moongy.RD.Launchpad.Data.Entities;

public class FeatureOnContractFeatureGroup
{
    [Key]
    public int Id { get; set; }

    public Guid UUid { get; set; }

    [ForeignKey(nameof(ContractFeature))]
    public int ContractFeatureId { get; set; }


    [ForeignKey(nameof(ContractFeatureGroup))]
    public int ContractFeatureGroupId { get; set; }

    public virtual ContractFeature? ContractFeature { get; set; }

    public virtual ContractFeatureGroup? ContractFeatureGroup { get; set; }

}
