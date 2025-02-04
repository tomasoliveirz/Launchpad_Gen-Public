using Moongy.RD.Launchpad.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Moongy.RD.Launchpad.Data.Entities;

public class BlockchainNetwork : CommonAtributes
{
    [Key]
    public int Id { get; set; }

    public Guid UUid { get; set; }

    public Blob Image { get; set; }

    public virtual ICollection<PublishResult> PublishResults { get; set; } = [];
}
