using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class DatabaseSeedingExtensions
{
    public static void ApplyDatabaseSeeding(this ModelBuilder builder)
    {
        // 1. IAM Seeding
        builder.Entity<Organization>().HasData(
            new { Id = 101, Name = "Hospital Central CortiSense", Ruc = "1012345678901", Email = "admin101@hospitalcentral.com", Phone = "555-0101", Address = "Av. Principal 123", Status = "ACTIVE", RegistrationStatus = "COMPLETED" }
        );

        builder.Entity<User>().HasData(
            new { Id = 101, OrganizationId = 101, FirstName = "Admin", LastName = "User", Email = "admin101@hospitalcentral.com", PasswordHash = "dummyhash", Role = "Admin", Status = "ACTIVE", RegistrationStatus = "COMPLETED" },
            new { Id = 102, OrganizationId = 101, FirstName = "Carlos", LastName = "Supervisor", Email = "supervisor101@hospitalcentral.com", PasswordHash = "dummyhash", Role = "Clinical Supervisor", Status = "ACTIVE", RegistrationStatus = "COMPLETED" },
            new { Id = 103, OrganizationId = 101, FirstName = "Laura", LastName = "Gomez", Email = "lgomez101@hospitalcentral.com", PasswordHash = "dummyhash", Role = "Medical Staff", Status = "ACTIVE", RegistrationStatus = "COMPLETED" },
            new { Id = 104, OrganizationId = 101, FirstName = "Juan", LastName = "Perez", Email = "jperez101@hospitalcentral.com", PasswordHash = "dummyhash", Role = "Medical Staff", Status = "ACTIVE", RegistrationStatus = "COMPLETED" }
        );

        // 2. Subscription Billing Seeding
        builder.Entity<Plan>().HasData(
            new { Id = 101, Code = "FREE_101", Name = "Free Trial Seed", Price = 0m, Currency = "USD", BillingPeriod = SyncedHealth.Center.Platform.Subscription.Domain.Model.ValueObjects.EBillingPeriod.Monthly, MaxSupervisors = 1, MaxTeams = 1, MaxWorkAreas = 1, MonthlyInvitations = 5, DataHistoryDays = 7, SupportLevel = SyncedHealth.Center.Platform.Subscription.Domain.Model.ValueObjects.ESupportLevel.Standard, Recommended = false, FeatureKeys = Array.Empty<string>(), EnabledModules = Array.Empty<string>(), DisabledModules = Array.Empty<string>() },
            new { Id = 102, Code = "PREMIUM_101", Name = "Premium Enterprise Seed", Price = 999.99m, Currency = "USD", BillingPeriod = SyncedHealth.Center.Platform.Subscription.Domain.Model.ValueObjects.EBillingPeriod.Yearly, MaxSupervisors = 50, MaxTeams = 100, MaxWorkAreas = 50, MonthlyInvitations = 500, DataHistoryDays = 365, SupportLevel = SyncedHealth.Center.Platform.Subscription.Domain.Model.ValueObjects.ESupportLevel.Priority, Recommended = true, FeatureKeys = Array.Empty<string>(), EnabledModules = Array.Empty<string>(), DisabledModules = Array.Empty<string>() }
        );

        builder.Entity<SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates.Subscription>().HasData(
            new { Id = 101, OrganizationId = 101, PlanId = 102, Status = SyncedHealth.Center.Platform.Subscription.Domain.Model.ValueObjects.ESubscriptionStatus.Active, StartedAt = DateTimeOffset.UtcNow }
        );

        // 3. Shift Coordination Seeding
        builder.Entity<ShiftRecord>().HasData(
            new { Id = 101, OrganizationId = 101, UserId = 103, WorkAreaId = 101, Type = "DAY", Status = "SCHEDULED", ScheduledStart = DateTimeOffset.UtcNow.AddDays(1), ScheduledEnd = DateTimeOffset.UtcNow.AddDays(1).AddHours(8) },
            new { Id = 102, OrganizationId = 101, UserId = 104, WorkAreaId = 101, Type = "NIGHT", Status = "SCHEDULED", ScheduledStart = DateTimeOffset.UtcNow.AddDays(1).AddHours(20), ScheduledEnd = DateTimeOffset.UtcNow.AddDays(2).AddHours(4) },
            // Turno actual in-progress para generar riesgo
            new { Id = 103, OrganizationId = 101, UserId = 103, WorkAreaId = 102, Type = "EMERGENCY", Status = "IN_PROGRESS", ScheduledStart = DateTimeOffset.UtcNow.AddHours(-10), ScheduledEnd = DateTimeOffset.UtcNow.AddHours(2), CheckInAt = DateTimeOffset.UtcNow.AddHours(-10) }
        );

        // 4. Clinical Risk Assessment Seeding
        builder.Entity<RiskAssessment>().HasData(
            new { Id = 101, OrganizationId = 101, UserId = 103, FatigueLevel = 85, RiskLevel = "HIGH", HeartRate = 120, Hrv = 20, LastUpdatedAt = DateTimeOffset.UtcNow },
            new { Id = 102, OrganizationId = 101, UserId = 104, FatigueLevel = 20, RiskLevel = "LOW", HeartRate = 70, Hrv = 60, LastUpdatedAt = DateTimeOffset.UtcNow }
        );

        // 5. Staff Recovery Seeding
        builder.Entity<RecoveryPlan>().HasData(
            new { Id = 101, MedicalStaffId = 103, Description = "Alta Fatiga", SuggestedRestDays = 1, Status = "PENDING" }
        );
    }
}
