using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.StaffRecovery.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyStaffRecoveryConfiguration(this ModelBuilder builder)
    {
        builder.Entity<RecoveryPlan>().ToTable("recovery_plans");

        builder.Entity<RecoveryPlan>().HasKey(recoveryPlan => recoveryPlan.Id);

        builder.Entity<RecoveryPlan>()
            .Property(recoveryPlan => recoveryPlan.Id)
            .HasColumnName("id")
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<RecoveryPlan>()
            .Property(recoveryPlan => recoveryPlan.MedicalStaffId)
            .HasColumnName("medical_staff_id")
            .IsRequired();

        builder.Entity<RecoveryPlan>()
            .Property(recoveryPlan => recoveryPlan.Description)
            .HasColumnName("description")
            .HasMaxLength(500)
            .IsRequired();

        builder.Entity<RecoveryPlan>()
            .Property(recoveryPlan => recoveryPlan.SuggestedRestDays)
            .HasColumnName("suggested_rest_days")
            .IsRequired();

        builder.Entity<RecoveryPlan>()
            .Property(recoveryPlan => recoveryPlan.Status)
            .HasColumnName("status")
            .HasMaxLength(50)
            .IsRequired();

        builder.Entity<RecoveryPlan>()
            .Property(recoveryPlan => recoveryPlan.CreatedAt)
            .HasColumnName("created_at");

        builder.Entity<RecoveryPlan>()
            .Property(recoveryPlan => recoveryPlan.UpdatedAt)
            .HasColumnName("updated_at");

        builder.Entity<RecoveryPlan>()
            .HasIndex(recoveryPlan => recoveryPlan.MedicalStaffId);

        builder.Entity<RecoveryPlan>()
            .HasIndex(recoveryPlan => recoveryPlan.Status);

        builder.Entity<RecoveryPlan>()
            .HasIndex(recoveryPlan => recoveryPlan.SuggestedRestDays);
    }
}