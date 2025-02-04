using Moongy.RD.Launchpad.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace Moongy.RD.Launchpad.Data.Entities;

public class ContractType : EntityWithNameAndDescription
{
    public virtual ICollection<ContractVariant> ContractVariants { get; set; } = [];
}
