using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.AuditCompliance.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
/// Provides Entity Framework Core configuration for the Audit Compliance bounded context.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    /// Applies the Audit Compliance bounded context configuration.
    /// </summary>
    /// <param name="builder">The model builder.</param>
    public static void ApplyAuditComplianceConfiguration(this ModelBuilder builder)
    {
        // Audit Compliance Context
        builder.Entity<AuditLog>().ToTable("audit_logs");
        builder.Entity<AuditLog>().HasKey(a => a.Id);
        
        builder.Entity<AuditLog>().Property(a => a.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<AuditLog>().Property(a => a.OrganizationId)
            .IsRequired();

        builder.Entity<AuditLog>().Property(a => a.ActorUserId)
            .IsRequired();

        builder.Entity<AuditLog>().Property(a => a.Type)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(60);

        builder.Entity<AuditLog>().Property(a => a.Severity)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Entity<AuditLog>().Property(a => a.ResourceType)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(60);

        builder.Entity<AuditLog>().Property(a => a.ResourceId)
            .IsRequired();

        builder.Entity<AuditLog>().Property(a => a.Source)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(60);

        builder.Entity<AuditLog>().Property(a => a.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Entity<AuditLog>().Property(a => a.CreatedAt);

        builder.Entity<AuditLog>().Property(a => a.UpdatedAt);
    }
}