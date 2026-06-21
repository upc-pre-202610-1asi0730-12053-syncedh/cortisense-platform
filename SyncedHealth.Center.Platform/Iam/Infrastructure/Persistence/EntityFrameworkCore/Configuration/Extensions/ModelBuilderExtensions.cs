using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        ApplyUserConfiguration(builder);
        ApplyOrganizationConfiguration(builder);
    }

    private static void ApplyUserConfiguration(ModelBuilder builder)
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

    private static void ApplyOrganizationConfiguration(ModelBuilder builder)
    {
        builder.Entity<Organization>().ToTable("organizations");

        builder.Entity<Organization>().HasKey(organization => organization.Id);

        builder.Entity<Organization>().Property(organization => organization.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<Organization>().Property(organization => organization.Name)
            .IsRequired()
            .HasMaxLength(120);

        builder.Entity<Organization>().Property(organization => organization.Ruc)
            .IsRequired()
            .HasMaxLength(20);

        builder.Entity<Organization>().HasIndex(organization => organization.Ruc)
            .IsUnique();

        builder.Entity<Organization>().Property(organization => organization.Email)
            .IsRequired()
            .HasMaxLength(160);

        builder.Entity<Organization>().HasIndex(organization => organization.Email)
            .IsUnique();

        builder.Entity<Organization>().Property(organization => organization.Phone)
            .IsRequired()
            .HasMaxLength(30);

        builder.Entity<Organization>().Property(organization => organization.Address)
            .IsRequired()
            .HasMaxLength(250);

        builder.Entity<Organization>().Property(organization => organization.Status)
            .IsRequired()
            .HasMaxLength(30);

        builder.Entity<Organization>().Property(organization => organization.RegistrationStatus)
            .IsRequired()
            .HasMaxLength(30);

        builder.Entity<Organization>().Property(organization => organization.ActivatedAt);

        builder.Entity<Organization>().Property(organization => organization.CancelledAt);

        builder.Entity<Organization>().Property(organization => organization.CreatedAt);

        builder.Entity<Organization>().Property(organization => organization.UpdatedAt);
    }
}