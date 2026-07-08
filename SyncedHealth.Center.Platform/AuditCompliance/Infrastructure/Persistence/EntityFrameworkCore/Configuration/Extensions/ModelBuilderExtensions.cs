using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.AuditCompliance.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
/// Represents the model builder extensions in the CortiSense Platform.
/// </summary>
public static class ModelBuilderExtensions
{
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