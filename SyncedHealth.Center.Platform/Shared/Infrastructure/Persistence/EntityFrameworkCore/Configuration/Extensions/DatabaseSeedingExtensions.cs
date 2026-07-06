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
            new { Id = 104, OrganizationId = 101, FirstName = "Juan", LastName = "Perez", Email = "jperez101@hospitalcentral.com", PasswordHash = "dummyhash", Role = "Medical Staff", Status = "ACTIVE", RegistrationStatus = "COMPLETED" },
            new { Id = 205, OrganizationId = 101, FirstName = "Ana", LastName = "Torres", Email = "atorres101@hospitalcentral.com", PasswordHash = "dummyhash", Role = "Medical Staff", Status = "ACTIVE", RegistrationStatus = "COMPLETED" },
            new { Id = 206, OrganizationId = 101, FirstName = "Luis", LastName = "Ramirez", Email = "lramirez101@hospitalcentral.com", PasswordHash = "dummyhash", Role = "Medical Staff", Status = "ACTIVE", RegistrationStatus = "COMPLETED" },
            new { Id = 207, OrganizationId = 101, FirstName = "Maria", LastName = "Lopez", Email = "mlopez101@hospitalcentral.com", PasswordHash = "dummyhash", Role = "Clinical Supervisor", Status = "ACTIVE", RegistrationStatus = "COMPLETED" }
        );

        // 2. Subscription Billing Seeding
        builder.Entity<SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates.Subscription>().HasData(
            new { Id = 101, OrganizationId = 101, PlanId = 3, Status = SyncedHealth.Center.Platform.Subscription.Domain.Model.ValueObjects.ESubscriptionStatus.Active, StartedAt = DateTimeOffset.Parse("2026-07-06T00:00:00Z") }
        );

        // 3. Shift Coordination Seeding
        builder.Entity<WorkArea>().HasData(
            new { Id = 1001, Name = "Emergencias" },
            new { Id = 1002, Name = "Cuidados Intensivos (UCI)" },
            new { Id = 1003, Name = "Cirugía" }
        );

        builder.Entity<CareTeam>().HasData(
            new { Id = 1001, OrganizationId = 101, Name = "Equipo de Emergencias Alpha", WorkAreaId = 1001, SupervisorId = 102, Status = "ACTIVE" },
            new { Id = 1002, OrganizationId = 101, Name = "Equipo UCI Beta", WorkAreaId = 1002, SupervisorId = 207, Status = "ACTIVE" }
        );

        builder.Entity<TeamMember>().HasData(
            new { Id = 1001, TeamId = 1001, UserId = 103 },
            new { Id = 1002, TeamId = 1001, UserId = 104 },
            new { Id = 1003, TeamId = 1002, UserId = 205 },
            new { Id = 1004, TeamId = 1002, UserId = 206 }
        );

        builder.Entity<ShiftRecord>().HasData(
            new { Id = 101, OrganizationId = 101, UserId = 103, WorkAreaId = 2, Type = "DAY", Status = "SCHEDULED", ScheduledStart = DateTimeOffset.Parse("2026-07-06T00:00:00Z").AddDays(1), ScheduledEnd = DateTimeOffset.Parse("2026-07-06T00:00:00Z").AddDays(1).AddHours(8) },
            new { Id = 102, OrganizationId = 101, UserId = 104, WorkAreaId = 1, Type = "NIGHT", Status = "SCHEDULED", ScheduledStart = DateTimeOffset.Parse("2026-07-06T00:00:00Z").AddDays(1).AddHours(20), ScheduledEnd = DateTimeOffset.Parse("2026-07-06T00:00:00Z").AddDays(2).AddHours(4) },
            new { Id = 103, OrganizationId = 101, UserId = 103, WorkAreaId = 3, Type = "EMERGENCY", Status = "IN_PROGRESS", ScheduledStart = DateTimeOffset.Parse("2026-07-06T00:00:00Z").AddHours(-10), ScheduledEnd = DateTimeOffset.Parse("2026-07-06T00:00:00Z").AddHours(2), CheckInAt = DateTimeOffset.Parse("2026-07-06T00:00:00Z").AddHours(-10) },
            new { Id = 204, OrganizationId = 101, UserId = 205, WorkAreaId = 2, Type = "DAY", Status = "COMPLETED", ScheduledStart = DateTimeOffset.Parse("2026-07-06T00:00:00Z").AddDays(-1), ScheduledEnd = DateTimeOffset.Parse("2026-07-06T00:00:00Z").AddDays(-1).AddHours(8), CheckInAt = DateTimeOffset.Parse("2026-07-06T00:00:00Z").AddDays(-1), CheckOutAt = DateTimeOffset.Parse("2026-07-06T00:00:00Z").AddDays(-1).AddHours(8) },
            new { Id = 205, OrganizationId = 101, UserId = 206, WorkAreaId = 1, Type = "DAY", Status = "SCHEDULED", ScheduledStart = DateTimeOffset.Parse("2026-07-06T00:00:00Z").AddDays(2), ScheduledEnd = DateTimeOffset.Parse("2026-07-06T00:00:00Z").AddDays(2).AddHours(8) }
        );

        // 4. Clinical Risk Assessment Seeding
        builder.Entity<RiskAssessment>().HasData(
            new { Id = 101, OrganizationId = 101, UserId = 103, FatigueLevel = 85, RiskLevel = "HIGH", HeartRate = 120, Hrv = 20, LastUpdatedAt = DateTimeOffset.Parse("2026-07-06T00:00:00Z") },
            new { Id = 102, OrganizationId = 101, UserId = 104, FatigueLevel = 20, RiskLevel = "LOW", HeartRate = 70, Hrv = 60, LastUpdatedAt = DateTimeOffset.Parse("2026-07-06T00:00:00Z") },
            new { Id = 203, OrganizationId = 101, UserId = 205, FatigueLevel = 45, RiskLevel = "MEDIUM", HeartRate = 95, Hrv = 40, LastUpdatedAt = DateTimeOffset.Parse("2026-07-06T00:00:00Z") },
            new { Id = 204, OrganizationId = 101, UserId = 206, FatigueLevel = 10, RiskLevel = "LOW", HeartRate = 65, Hrv = 70, LastUpdatedAt = DateTimeOffset.Parse("2026-07-06T00:00:00Z") }
        );

        // 5. Staff Recovery Seeding
        builder.Entity<RecoveryPlan>().HasData(
            new { Id = 101, MedicalStaffId = 103, Description = "Alta Fatiga", SuggestedRestDays = 1, Status = "PENDING" },
            new { Id = 202, MedicalStaffId = 205, Description = "Fatiga media detectada", SuggestedRestDays = 1, Status = "COMPLETED" }
        );
    }
}
