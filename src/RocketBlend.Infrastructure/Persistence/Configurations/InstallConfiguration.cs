using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RocketBlend.Domain.Entities;

namespace RocketBlend.Infrastructure.Persistence.Configurations;

/// <summary>
/// The Install entity configuration.
/// </summary>
public class InstallConfiguration : IEntityTypeConfiguration<Install>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Install> builder)
    {
        builder.ToTable("Install");
        builder.Ignore(e => e.DomainEvents);

        builder.HasOne(p => p.Build)
            .WithMany(r => r.Installs)
            .HasForeignKey(p => p.BuildId)
            .IsRequired();

        builder.Property(t => t.FileName)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(t => t.FilePath)
            .IsRequired()
            .HasMaxLength(512);

        builder.Property(t => t.LaunchArgs)
            .IsRequired()
            .HasMaxLength(512);
    }
}