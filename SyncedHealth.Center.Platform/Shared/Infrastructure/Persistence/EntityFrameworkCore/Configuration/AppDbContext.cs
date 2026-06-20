using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using SyncedHealth.Center.Platform.StaffRecovery.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using SyncedHealth.Center.Platform.StaffRecovery.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

/// <summary>
///     Application database context for the Learning Center Platform
/// </summary>
/// <param name="options">
///     The options for the database context
/// </param>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Apply audit timestamp interceptor for all IAuditableEntity implementations
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }


    /// <summary>
    ///     On creating the database model
    /// </summary>
    /// <remarks>
    ///     This method is used to create the database model for the application.
    /// </remarks>
    /// <param name="builder">
    ///     The model builder for the database context
    /// </param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // IAM Context
        builder.ApplyIamConfiguration();
        
        // Clinical Risk Assessment Context
        builder.ApplyClinicalRiskAssessmentConfiguration();
        
        // Staff Recovery Context
        builder.ConfigureStaffRecoveryContext();
        
        // General Naming Convention for the database objects
        builder.UseSnakeCaseNamingConvention();
    }
}
