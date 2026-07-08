using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
/// Represents the model builder extensions in the CortiSense Platform.
/// </summary>
public static class ModelBuilderExtensions
{
    public static void ApplyClinicalRiskAssessmentConfiguration(this ModelBuilder builder)
    {
        builder.Entity<RiskAssessment>().HasKey(r => r.Id);
        builder.Entity<RiskAssessment>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<RiskAssessment>().Property(r => r.OrganizationId).IsRequired();
        builder.Entity<RiskAssessment>().Property(r => r.UserId).IsRequired();
        builder.Entity<RiskAssessment>().Property(r => r.FatigueLevel).IsRequired();
        builder.Entity<RiskAssessment>().Property(r => r.RiskLevel).IsRequired().HasMaxLength(30);
        builder.Entity<RiskAssessment>().Property(r => r.HeartRate).IsRequired();
        builder.Entity<RiskAssessment>().Property(r => r.Hrv).IsRequired();
        builder.Entity<RiskAssessment>().Property(r => r.LastUpdatedAt).IsRequired();

        builder.Entity<ClinicalAlert>().HasKey(a => a.Id);
        builder.Entity<ClinicalAlert>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ClinicalAlert>().Property(a => a.OrganizationId).IsRequired();
        builder.Entity<ClinicalAlert>().Property(a => a.UserId).IsRequired();
        builder.Entity<ClinicalAlert>().Property(a => a.Severity).IsRequired().HasMaxLength(30);
        builder.Entity<ClinicalAlert>().Property(a => a.Status).IsRequired().HasMaxLength(30);
        builder.Entity<ClinicalAlert>().Property(a => a.Message).IsRequired().HasMaxLength(300);
        builder.Entity<ClinicalAlert>().Property(a => a.CreatedAt).IsRequired();

        builder.Entity<VitalSignAnomaly>().HasKey(a => a.Id);
        builder.Entity<VitalSignAnomaly>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<VitalSignAnomaly>().Property(a => a.OrganizationId).IsRequired();
        builder.Entity<VitalSignAnomaly>().Property(a => a.UserId).IsRequired();
        builder.Entity<VitalSignAnomaly>().Property(a => a.Type).IsRequired().HasMaxLength(80);
        builder.Entity<VitalSignAnomaly>().Property(a => a.Severity).IsRequired().HasMaxLength(30);
        builder.Entity<VitalSignAnomaly>().Property(a => a.Status).IsRequired().HasMaxLength(30);
        builder.Entity<VitalSignAnomaly>().Property(a => a.Value).IsRequired().HasMaxLength(80);
        builder.Entity<VitalSignAnomaly>().Property(a => a.Threshold).IsRequired().HasMaxLength(80);
        builder.Entity<VitalSignAnomaly>().Property(a => a.Message).IsRequired().HasMaxLength(300);
        builder.Entity<VitalSignAnomaly>().Property(a => a.DetectedAt).IsRequired();

        builder.Entity<VitalSignReading>().HasKey(r => r.Id);
        builder.Entity<VitalSignReading>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<VitalSignReading>().Property(r => r.OrganizationId).IsRequired();
        builder.Entity<VitalSignReading>().Property(r => r.UserId).IsRequired();
        builder.Entity<VitalSignReading>().Property(r => r.HeartRate).IsRequired();
        builder.Entity<VitalSignReading>().Property(r => r.Hrv).IsRequired();
        builder.Entity<VitalSignReading>().Property(r => r.FatigueLevel).IsRequired();
        builder.Entity<VitalSignReading>().Property(r => r.CortisolLevel).IsRequired().HasPrecision(10, 2);
        builder.Entity<VitalSignReading>().Property(r => r.SensorStatus).IsRequired().HasMaxLength(30);
        builder.Entity<VitalSignReading>().Property(r => r.RecordedAt).IsRequired();
    }
}