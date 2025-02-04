using System.ComponentModel.DataAnnotations;

namespace Moongy.RD.Launchpad.Data.Entities;

public class ContractFeature
{
    [Key]
    public int Id { get; set; }

    public Guid UUid { get; set; }

    public string? Name { get; set; }

    public DataType DataType { get; set; }
    
    public virtual ICollection<FeatureOnContractFeatureGroup> FeatureOnContractFeatureGroups { get; set; } = [];
}
