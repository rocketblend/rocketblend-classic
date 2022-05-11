using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RocketBlend.Domain.Entities;

namespace RocketBlend.Infrastructure.Persistence.Configurations;

/// <summary>
/// The build entity configuration.
/// </summary>
public class BuildConfiguration : IEntityTypeConfiguration<Build>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Build> builder)
    {
        builder.ToTable("Build");
        builder.Ignore(e => e.DomainEvents);     

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(256);
        builder.Property(t => t.Tag)
            .IsRequired()
            .HasMaxLength(256);
        builder.Property(t => t.Hash)
            .IsRequired()
            .HasMaxLength(256);
        builder.Property(t => t.DownloadUrl)
            .IsRequired()
            .HasMaxLength(256);
        builder.Property(t => t.Filesize)
            .IsRequired()
            .HasMaxLength(64);
    }
}