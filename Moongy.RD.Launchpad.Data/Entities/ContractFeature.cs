using System.Text.Json.Serialization;
using Moongy.RD.Launchpad.Data.Base;

namespace Moongy.RD.Launchpad.Data.Entities;

public class ContractFeature : EntityWithNameAndDescription
{
    public string? DataType { get; set; }
    
    [JsonIgnore]
    public virtual ICollection<FeatureOnContractFeatureGroup> FeatureOnContractFeatureGroups { get; set; } = [];
}
