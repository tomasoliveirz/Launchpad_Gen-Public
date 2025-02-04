using Moongy.RD.Launchpad.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace Moongy.RD.Launchpad.Data.Entities;

public class ContractType : CommonAtributes
{
    [Key]
    public int Id { get; set; }

    public Guid UUid { get; set; }

    public virtual ICollection<ContractVariant> ContractVariants { get; set; } = [];
}
