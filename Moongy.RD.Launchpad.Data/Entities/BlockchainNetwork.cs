using System.ComponentModel.DataAnnotations;

namespace Moongy.RD.Launchpad.Data.Entities;

public class BlockchainNetwork
{
    [Key]
    public int Id { get; set; }

    public Guid UUid { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<PublishResult> PublishResults { get; set; } = [];
}
