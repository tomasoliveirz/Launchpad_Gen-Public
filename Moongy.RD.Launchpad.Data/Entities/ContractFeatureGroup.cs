using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Moongy.RD.Launchpad.Data.Base;

namespace Moongy.RD.Launchpad.Data.Entities;

public class ContractFeatureGroup : EntityWithNameAndDescription
{
    public string? DataType { get; set; }

    [ForeignKey(nameof(ContractVariant))]
    public int ContractVariantId { get; set; }
    public virtual ContractVariant? ContractVariant{ get; set; }

    [JsonIgnore]
    public virtual ICollection<FeatureOnContractFeatureGroup> FeaturesOnContractFeatureGroup { get; set; } = [];
}
