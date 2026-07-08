using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
/// Represents the model builder extensions in the CortiSense Platform.
/// </summary>
public static class ModelBuilderExtensions
{
    public static void ApplyShiftCoordinationConfiguration(this ModelBuilder builder)
    {
        // Shift Record Aggregate
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

        builder.Entity<ShiftRecord>().HasIndex(shiftRecord => shiftRecord.OrganizationId);

        builder.Entity<ShiftRecord>().HasIndex(shiftRecord => shiftRecord.UserId);

        builder.Entity<ShiftRecord>().HasIndex(shiftRecord => shiftRecord.WorkAreaId);

        // Work Area Aggregate
        builder.Entity<WorkArea>().ToTable("work_areas");

        builder.Entity<WorkArea>().HasKey(workArea => workArea.Id);

        builder.Entity<WorkArea>().Property(workArea => workArea.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<WorkArea>().Property(workArea => workArea.Name)
            .IsRequired()
            .HasMaxLength(120);

        builder.Entity<WorkArea>().Property(workArea => workArea.CreatedAt);

        builder.Entity<WorkArea>().Property(workArea => workArea.UpdatedAt);

        // Specialty Aggregate
        builder.Entity<Specialty>().ToTable("specialties");

        builder.Entity<Specialty>().HasKey(specialty => specialty.Id);

        builder.Entity<Specialty>().Property(specialty => specialty.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<Specialty>().Property(specialty => specialty.Name)
            .IsRequired()
            .HasMaxLength(120);

        builder.Entity<Specialty>().Property(specialty => specialty.CreatedAt);

        builder.Entity<Specialty>().Property(specialty => specialty.UpdatedAt);

        builder.Entity<Specialty>().HasIndex(specialty => specialty.Name);

        // Care Team Aggregate
        builder.Entity<CareTeam>().ToTable("care_teams");

        builder.Entity<CareTeam>().HasKey(careTeam => careTeam.Id);

        builder.Entity<CareTeam>().Property(careTeam => careTeam.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<CareTeam>().Property(careTeam => careTeam.OrganizationId)
            .IsRequired();

        builder.Entity<CareTeam>().Property(careTeam => careTeam.Name)
            .IsRequired()
            .HasMaxLength(120);

        builder.Entity<CareTeam>().Property(careTeam => careTeam.WorkAreaId)
            .IsRequired();

        builder.Entity<CareTeam>().Property(careTeam => careTeam.SupervisorId)
            .IsRequired();

        builder.Entity<CareTeam>().Property(careTeam => careTeam.Status)
            .IsRequired()
            .HasMaxLength(30);

        builder.Entity<CareTeam>().Property(careTeam => careTeam.CreatedAt);

        builder.Entity<CareTeam>().Property(careTeam => careTeam.UpdatedAt);

        builder.Entity<CareTeam>().HasIndex(careTeam => careTeam.OrganizationId);

        builder.Entity<CareTeam>().HasIndex(careTeam => careTeam.WorkAreaId);

        builder.Entity<CareTeam>().HasIndex(careTeam => careTeam.SupervisorId);

        // Team Member Aggregate
        builder.Entity<TeamMember>().ToTable("team_members");

        builder.Entity<TeamMember>().HasKey(teamMember => teamMember.Id);

        builder.Entity<TeamMember>().Property(teamMember => teamMember.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<TeamMember>().Property(teamMember => teamMember.TeamId)
            .IsRequired();

        builder.Entity<TeamMember>().Property(teamMember => teamMember.UserId)
            .IsRequired();

        builder.Entity<TeamMember>().Property(teamMember => teamMember.CreatedAt);

        builder.Entity<TeamMember>().Property(teamMember => teamMember.UpdatedAt);

        builder.Entity<TeamMember>().HasIndex(teamMember => teamMember.TeamId);

        builder.Entity<TeamMember>().HasIndex(teamMember => teamMember.UserId);

        builder.Entity<TeamMember>()
            .HasIndex(teamMember => new { teamMember.TeamId, teamMember.UserId })
            .IsUnique();
    }
}