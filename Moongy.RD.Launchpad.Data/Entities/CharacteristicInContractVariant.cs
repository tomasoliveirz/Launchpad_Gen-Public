using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Moongy.RD.Launchpad.Data.Base;

namespace Moongy.RD.Launchpad.Data.Entities;

public class CharacteristicInContractVariant : Entity
{

    [ForeignKey(nameof(ContractVariant))]
    public int ContractVariantId { get; set; }

    [ForeignKey(nameof(ContractCharacteristic))]
    public int ContractCharacteristicId { get; set; }

    public virtual ContractVariant? ContractVariant { get; set; }

    public virtual ContractCharacteristic? ContractCharacteristic { get; set; }
    public string Value { get; set; }
}
