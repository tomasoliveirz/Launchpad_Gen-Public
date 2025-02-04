using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moongy.RD.Launchpad.Data.Entities;
public class ContractVariant
{
    [Key]
    public int Id { get; set; }

    public Guid UUid { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }


    [ForeignKey(nameof(ContractType))]
    public int ContractTypeId { get; set; }

    public virtual ContractType? ContractType { get; set; }

    public virtual ICollection<CharacteristicInContractVariant> CharacteristicInContractVariants { get; set; } = [];

    public virtual ICollection<ContractGenerationResult> ContractGenerationResults { get; set; } = [];
}
