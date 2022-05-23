using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RocketBlend.Domain.Entities;

namespace RocketBlend.Infrastructure.Persistence.Configurations;

/// <summary>
/// The resource entity configuration.
/// </summary>
public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Project");
        builder.Ignore(e => e.DomainEvents);

        builder.HasOne(p => p.Install)
            .WithMany(r => r.Projects)
            .HasForeignKey(p => p.InstallId);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(t => t.FileName)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(t => t.FilePath)
            .IsRequired()
            .HasMaxLength(512);
    }
}