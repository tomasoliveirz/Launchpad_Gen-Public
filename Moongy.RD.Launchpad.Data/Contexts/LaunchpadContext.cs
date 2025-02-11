using Microsoft.EntityFrameworkCore;
using Moongy.RD.Launchpad.Data.Entities;

namespace Moongy.RD.Launchpad.Data.Contexts;

public class LaunchpadContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContractType>().Property(x => x.Uuid).HasDefaultValueSql("NEWID()");
        modelBuilder.Entity<GenerationFeatureValue>().HasOne<FeatureOnContractFeatureGroup>().WithMany(x => x.ContractGenerationFeatureValues).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<GenerationFeatureValue>().HasOne(x => x.ContractGenerationResult).WithMany(x => x.ContractGenerationFeatureValues).OnDelete(DeleteBehavior.NoAction);
    }

    public DbSet<ContractType> ContractTypes { get; set; }
    public DbSet<GenerationFeatureValue> GenerationFeatureValues { get; set; }

    public DbSet<ContractVariant> ContractsVariants { get; set; }

    public DbSet<CharacteristicInContractVariant> CharacteristicInContractVariants { get; set; }

    public DbSet<ContractCharacteristic> ContractCharacteristics { get; set; }

    public DbSet<ContractGenerationResult> ContractGenerationResults { get; set; }

    public DbSet<PublishResult> PublishResults { get; set; }

    public DbSet<BlockchainNetwork> BlockchainNetworks { get; set; }

    public DbSet<ContractFeatureGroup> ContractFeatureGroups { get; set; }

    public DbSet<FeatureOnContractFeatureGroup> FeatureOnContractsFeatures { get; set; }

    public DbSet<ContractFeature> ContractFeatures { get; set; }
}
