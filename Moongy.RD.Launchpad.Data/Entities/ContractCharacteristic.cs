using Moongy.RD.Launchpad.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace Moongy.RD.Launchpad.Data.Entities;

public class ContractCharacteristic : EntityWithNameAndDescription
{
    public virtual ICollection<CharacteristicInContractVariant> CharacteristicInContractVariants { get; set; } = [];
}
