using Moongy.RD.Launchpad.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moongy.RD.Launchpad.Data.Entities;
public class ContractVariant : EntityWithNameAndDescription
{

    [ForeignKey(nameof(ContractType))]
    public int ContractTypeId { get; set; }

    public virtual ContractType? ContractType { get; set; }

    public virtual ICollection<CharacteristicInContractVariant> CharacteristicInContractVariants { get; set; } = [];

    public virtual ICollection<ContractGenerationResult> ContractGenerationResults { get; set; } = [];
}
