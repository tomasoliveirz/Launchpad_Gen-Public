using Moongy.RD.Launchpad.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Moongy.RD.Launchpad.Data.Entities;
public class ContractVariant : EntityWithNameAndDescription
{

    [ForeignKey(nameof(ContractType))]
    public int ContractTypeId { get; set; }

    public virtual ContractType? ContractType { get; set; }

    [JsonIgnore]
    public virtual ICollection<CharacteristicInContractVariant> CharacteristicInContractVariants { get; set; } = [];

    [JsonIgnore]
    public virtual ICollection<ContractGenerationResult> ContractGenerationResults { get; set; } = [];
}
