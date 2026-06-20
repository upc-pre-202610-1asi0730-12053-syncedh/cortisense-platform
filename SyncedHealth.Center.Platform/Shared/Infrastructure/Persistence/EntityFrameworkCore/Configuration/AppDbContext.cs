using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.AuditCompliance.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using SyncedHealth.Center.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using SyncedHealth.Center.Platform.ShiftCoordination.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using SyncedHealth.Center.Platform.Subscription.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using SyncedHealth.Center.Platform.StaffRecovery.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;


namespace SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

/// <summary>
/// Application database context for the CortiSense Platform.
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddInterceptors(new AuditableEntityInterceptor());

        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // IAM Context
        builder.ApplyIamConfiguration();

        // Subscription Context
        builder.ApplySubscriptionConfiguration();

        // Clinical Risk Assessment Context
        builder.ApplyClinicalRiskAssessmentConfiguration();

        // Audit Compliance Context
        builder.ApplyAuditComplianceConfiguration();

        // Shift Coordination Context
        builder.ApplyShiftCoordinationConfiguration();
        
        // Staff Recovery Context
        builder.ConfigureStaffRecoveryContext();

        // General Naming Convention for the database objects
        builder.UseSnakeCaseNamingConvention();
    }
}