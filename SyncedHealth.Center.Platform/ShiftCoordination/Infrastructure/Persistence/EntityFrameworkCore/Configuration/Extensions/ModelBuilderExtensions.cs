using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyShiftCoordinationConfiguration(this ModelBuilder builder)
    {
        builder.Entity<ShiftRecord>().ToTable("shift_records");

        builder.Entity<ShiftRecord>().HasKey(shiftRecord => shiftRecord.Id);

        builder.Entity<ShiftRecord>().Property(shiftRecord => shiftRecord.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<ShiftRecord>().Property(shiftRecord => shiftRecord.OrganizationId)
            .IsRequired();

        builder.Entity<ShiftRecord>().Property(shiftRecord => shiftRecord.UserId)
            .IsRequired();

        builder.Entity<ShiftRecord>().Property(shiftRecord => shiftRecord.WorkAreaId)
            .IsRequired();

        builder.Entity<ShiftRecord>().Property(shiftRecord => shiftRecord.Type)
            .IsRequired()
            .HasMaxLength(30);

        builder.Entity<ShiftRecord>().Property(shiftRecord => shiftRecord.Status)
            .IsRequired()
            .HasMaxLength(30);

        builder.Entity<ShiftRecord>().Property(shiftRecord => shiftRecord.ScheduledStart)
            .IsRequired();

        builder.Entity<ShiftRecord>().Property(shiftRecord => shiftRecord.ScheduledEnd)
            .IsRequired();

        builder.Entity<ShiftRecord>().Property(shiftRecord => shiftRecord.CheckInAt);

        builder.Entity<ShiftRecord>().Property(shiftRecord => shiftRecord.CheckOutAt);

        builder.Entity<ShiftRecord>().Property(shiftRecord => shiftRecord.CreatedAt);

        builder.Entity<ShiftRecord>().Property(shiftRecord => shiftRecord.UpdatedAt);
    }
}