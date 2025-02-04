using Moongy.RD.Launchpad.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Moongy.RD.Launchpad.Data.Entities;

public class BlockchainNetwork : EntityWithNameAndDescription
{
    public string? Image { get; set; }

    public virtual ICollection<PublishResult> PublishResults { get; set; } = [];
}
