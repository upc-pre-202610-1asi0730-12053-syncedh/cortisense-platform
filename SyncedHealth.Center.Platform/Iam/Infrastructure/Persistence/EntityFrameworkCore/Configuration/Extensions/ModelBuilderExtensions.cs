using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        builder.Entity<User>().ToTable("users");

        builder.Entity<User>().HasKey(user => user.Id);

        builder.Entity<User>().Property(user => user.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<User>().Property(user => user.OrganizationId)
            .IsRequired();

        builder.Entity<User>().Property(user => user.FirstName)
            .IsRequired()
            .HasMaxLength(80);

        builder.Entity<User>().Property(user => user.LastName)
            .IsRequired()
            .HasMaxLength(80);

        builder.Entity<User>().Property(user => user.Email)
            .IsRequired()
            .HasMaxLength(160);

        builder.Entity<User>().HasIndex(user => user.Email)
            .IsUnique();

        builder.Entity<User>().Property(user => user.PasswordHash)
            .IsRequired();

        builder.Entity<User>().Property(user => user.Phone)
            .HasMaxLength(30);

        builder.Entity<User>().Property(user => user.WorkAreaId);

        builder.Entity<User>().Property(user => user.SpecialtyId);

        builder.Entity<User>().Property(user => user.Role)
            .IsRequired()
            .HasMaxLength(40);

        builder.Entity<User>().Property(user => user.Status)
            .IsRequired()
            .HasMaxLength(30);

        builder.Entity<User>().Property(user => user.RegistrationStatus)
            .IsRequired()
            .HasMaxLength(30);

        builder.Entity<User>().Property(user => user.ActivatedAt);

        builder.Entity<User>().Property(user => user.CancelledAt);

        builder.Entity<User>().Property(user => user.CreatedAt);

        builder.Entity<User>().Property(user => user.UpdatedAt);
    }
}