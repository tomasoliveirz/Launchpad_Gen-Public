using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Moongy.RD.Launchpad.Data.Base;

namespace Moongy.RD.Launchpad.Data.Entities;

public class PublishResult : Entity
{

    public string? Address { get; set; }

    public string? Bytecode { get; set; }

    public string? Abi { get; set; }

    [ForeignKey(nameof(ContractGenerationResult))]
    public int ContractGenerationResultId { get; set; }
    public virtual BlockchainNetwork? BlockchainNetwork { get; set; }


    [ForeignKey(nameof(BlockchainNetwork))]
    public int BlockchainNetworkId { get; set; }
    public virtual ContractGenerationResult? ContractGenerationResult { get; set; }
}
