using Microsoft.EntityFrameworkCore;
using Moongy.RD.Launchpad.Data.Entities;

namespace Moongy.RD.Launchpad.Data.Contexts;

public class LaunchpadContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContractType>().HasIndex(x => x.Uuid).IsUnique();
        modelBuilder.Entity<ContractType>().Property(x => x.Uuid).HasDefaultValueSql("NEWID()");
        modelBuilder.Entity<ContractVariant>().HasIndex(x => x.Uuid).IsUnique();
        modelBuilder.Entity<ContractVariant>().Property(x => x.Uuid).HasDefaultValueSql("NEWID()");
        modelBuilder.Entity<PublishResult>().Property(x => x.Uuid).HasDefaultValueSql("NEWID()");
        modelBuilder.Entity<BlockchainNetwork>().Property(x => x.Uuid).HasDefaultValueSql("NEWID()");
        modelBuilder.Entity<CharacteristicInContractVariant>().HasIndex(x => x.Uuid).IsUnique();
        modelBuilder.Entity<CharacteristicInContractVariant>().Property(x => x.Uuid).HasDefaultValueSql("NEWID()");
        modelBuilder.Entity<ContractCharacteristic>().Property(x => x.Uuid).HasDefaultValueSql("NEWID()");
        modelBuilder.Entity<ContractFeature>().Property(x => x.Uuid).HasDefaultValueSql("NEWID()");
        modelBuilder.Entity<ContractGenerationResult>().Property(x => x.Uuid).HasDefaultValueSql("NEWID()");
        modelBuilder.Entity<FeatureInContractType>().HasIndex(x => x.Uuid).IsUnique();
        modelBuilder.Entity<FeatureInContractType>().Property(x => x.Uuid).HasDefaultValueSql("NEWID()");
        modelBuilder.Entity<GenerationResultFeatureValue>().Property(x => x.Uuid).HasDefaultValueSql("NEWID()");
        modelBuilder.Entity<GenerationResultFeatureValue>()
        .HasOne(g => g.FeatureInContractType)
        .WithMany()
        .HasForeignKey(g => g.FeatureInContractTypeId)
        .OnDelete(DeleteBehavior.NoAction);
    }

    public DbSet<ContractType> ContractTypes { get; set; }

    public DbSet<ContractVariant> ContractsVariants { get; set; }

    public DbSet<CharacteristicInContractVariant> CharacteristicInContractVariants { get; set; }

    public DbSet<ContractCharacteristic> ContractCharacteristics { get; set; }

    public DbSet<ContractGenerationResult> ContractGenerationResults { get; set; }

    public DbSet<PublishResult> PublishResults { get; set; }

    public DbSet<BlockchainNetwork> BlockchainNetworks { get; set; }

    public DbSet<ContractFeature> ContractFeatures { get; set; }

    public DbSet<FeatureInContractType> FeatureInContractTypes { get; set; }
    public DbSet<GenerationResultFeatureValue> GenerationResultFeatureValues { get; set; }
}
