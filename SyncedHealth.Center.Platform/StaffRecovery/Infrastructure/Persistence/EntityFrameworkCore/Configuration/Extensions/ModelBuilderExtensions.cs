using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.StaffRecovery.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder ConfigureStaffRecoveryContext(this ModelBuilder builder)
    {

        builder.Entity<RecoveryPlan>().ToTable("recovery_plans");


        builder.Entity<RecoveryPlan>().HasKey(rp => rp.Id);
        builder.Entity<RecoveryPlan>().Property(rp => rp.Id).IsRequired().ValueGeneratedOnAdd();


        builder.Entity<RecoveryPlan>().Property(rp => rp.MedicalStaffId).IsRequired();
        builder.Entity<RecoveryPlan>().Property(rp => rp.Description).IsRequired().HasMaxLength(500);
        builder.Entity<RecoveryPlan>().Property(rp => rp.SuggestedRestDays).IsRequired();
        builder.Entity<RecoveryPlan>().Property(rp => rp.Status).IsRequired().HasMaxLength(50);

        return builder;
    }
}