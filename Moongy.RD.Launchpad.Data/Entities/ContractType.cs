using Moongy.RD.Launchpad.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Moongy.RD.Launchpad.Data.Entities;

public class ContractType : EntityWithNameAndDescription
{
    [JsonIgnore]
    public virtual ICollection<ContractVariant> ContractVariants { get; set; } = [];

    [JsonIgnore]
    public virtual ICollection<FeatureInContractType> FeatureInContractType { get; set; } = [];
}
