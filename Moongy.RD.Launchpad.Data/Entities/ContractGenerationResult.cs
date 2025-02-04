using Moongy.RD.Launchpad.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moongy.RD.Launchpad.Data.Entities;

public class ContractGenerationResult : EntityWithNameAndDescription
{

    public DateOnly CreateAt { get; set; }

    [ForeignKey(nameof(ContractVariant))]
    public int ContractVariantId { get; set; }

    public virtual ContractVariant? ContractVariant { get; set; }

    public virtual ICollection<PublishResult> PublishResults { get; set; } = [];

    public virtual ICollection<ContractFeatureGroup> ContractFeatureGroups { get; set; } = [];

}
