using Moongy.RD.Launchpad.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace Moongy.RD.Launchpad.Data.Entities;

public class ContractCharacteristic : CommonAtributes
{
    [Key]
    public int Id { get; set; }
    public Guid UUid { get; set; }

    public virtual ICollection<CharacteristicInContractVariant> CharacteristicInContractVariants { get; set; } = [];
}
