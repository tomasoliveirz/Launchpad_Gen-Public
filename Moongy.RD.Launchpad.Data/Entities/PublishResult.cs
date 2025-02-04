using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moongy.RD.Launchpad.Data.Entities;

public class PublishResult
{
    [Key]
    public int Id { get; set; }
    
    public Guid UUid { get; set; }

    public string? Address { get; set; }

    public string? Bytecode { get; set; }

    public string? Abi { get; set; }

    [ForeignKey(nameof(ContractGenerationResult))]
    public int ContractGenerationResultId { get; set; }

    [ForeignKey(nameof(BlockchainNetwork))]
    public int BlockchainNetworkId { get; set; }

    public virtual BlockchainNetwork? BlockchainNetwork { get; set; }

    public virtual ContractGenerationResult? ContractGenerationResult { get; set; }
}
