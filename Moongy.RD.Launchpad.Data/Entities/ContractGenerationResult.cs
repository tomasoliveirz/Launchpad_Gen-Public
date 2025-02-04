using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moongy.RD.Launchpad.Data.Entities;

public class ContractGenerationResult
{
    [Key]
    public int Id { get; set; }
    
    public Guid UUid { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateOnly CreateAt { get; set; }

    [ForeignKey(nameof(ContractVariant))]
    public int ContractVariantId { get; set; }

    public virtual ContractVariant? ContractVariant { get; set; }

    public virtual ICollection<PublishResult> PublishResults { get; set; } = [];

    public virtual ICollection<ContractFeatureGroup> ContractFeatureGroups { get; set; } = [];

}
