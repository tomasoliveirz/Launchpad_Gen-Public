using Moongy.RD.Launchpad.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moongy.RD.Launchpad.Data.Entities;
public class ContractVariant : CommonAtributes
{
    [Key]
    public int Id { get; set; }

    public Guid UUid { get; set; }


    [ForeignKey(nameof(ContractType))]
    public int ContractTypeId { get; set; }

    public virtual ContractType? ContractType { get; set; }

    public virtual ICollection<CharacteristicInContractVariant> CharacteristicInContractVariants { get; set; } = [];

    public virtual ICollection<ContractGenerationResult> ContractGenerationResults { get; set; } = [];
}
