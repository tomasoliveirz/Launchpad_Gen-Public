using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moongy.RD.Launchpad.Data.Entities;

public class CharacteristicInContractVariant
{
    [Key]
	public int Id { get; set; }

    public Guid UUid { get; set; }

    [ForeignKey(nameof(ContractVariant))]
    public int ContractVariantId { get; set; }

    [ForeignKey(nameof(ContractCharacteristic))]
    public int ContractCharacteristicId { get; set; }

    public virtual ContractVariant? ContractVariant { get; set; }

    public virtual ContractCharacteristic? ContractCharacteristic { get; set; }
}
