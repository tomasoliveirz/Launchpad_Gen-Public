using Moongy.RD.Launchpad.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Moongy.RD.Launchpad.Data.Entities;

public class ContractGenerationResult : EntityWithNameAndDescription
{

    public DateOnly CreateAt { get; set; }

    [ForeignKey(nameof(ContractVariant))]
    public int ContractVariantId { get; set; }

    public virtual ContractVariant? ContractVariant { get; set; }

    [JsonIgnore]
    public virtual ICollection<PublishResult> PublishResults { get; set; } = [];

    [JsonIgnore]
    public virtual ICollection<GenerationFeatureValue> ContractGenerationFeatureValues { get; set; } = [];

}
