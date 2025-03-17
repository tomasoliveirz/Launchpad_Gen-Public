using System.Text.Json.Serialization;
using Azure.Identity;
using Moongy.RD.Launchpad.Data.Base;

namespace Moongy.RD.Launchpad.Data.Entities;

public class ContractFeature : EntityWithNameAndDescription
{
    public string? DataType { get; set; }
    public string? DefaultValue { get; set; }
    public string? Options { get; set; }

    [JsonIgnore]
    public virtual ICollection<FeatureInContractType> FeatureInContractType { get; set; } = [];
}
