using Moongy.RD.Launchpad.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Moongy.RD.Launchpad.Data.Entities;

public class ContractCharacteristic : EntityWithNameAndDescription
{
    [JsonIgnore]
    public virtual ICollection<CharacteristicInContractVariant> CharacteristicInContractVariants { get; set; } = [];
}
