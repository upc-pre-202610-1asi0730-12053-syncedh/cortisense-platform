using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
/// Represents the model builder extensions in the CortiSense Platform.
/// </summary>
public static class ModelBuilderExtensions
{
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        ApplyUserConfiguration(builder);
        ApplyOrganizationConfiguration(builder);
        ApplyInvitationConfiguration(builder);
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

    private static void ApplyInvitationConfiguration(ModelBuilder builder)
    {
        builder.Entity<Invitation>().ToTable("invitations");

        builder.Entity<Invitation>().HasKey(invitation => invitation.Id);

        builder.Entity<Invitation>().Property(invitation => invitation.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<Invitation>().Property(invitation => invitation.OrganizationId)
            .IsRequired();

        builder.Entity<Invitation>().Property(invitation => invitation.Email)
            .IsRequired()
            .HasMaxLength(160);

        builder.Entity<Invitation>().Property(invitation => invitation.Role)
            .IsRequired()
            .HasMaxLength(40);

        builder.Entity<Invitation>().Property(invitation => invitation.Status)
            .IsRequired()
            .HasMaxLength(30);

        builder.Entity<Invitation>().Property(invitation => invitation.Token)
            .IsRequired()
            .HasMaxLength(120);

        builder.Entity<Invitation>().HasIndex(invitation => invitation.Token)
            .IsUnique();

        builder.Entity<Invitation>().Property(invitation => invitation.EmailStatus)
            .IsRequired()
            .HasMaxLength(30);

        builder.Entity<Invitation>().Property(invitation => invitation.ResendEmailId)
            .HasMaxLength(160);

        builder.Entity<Invitation>().Property(invitation => invitation.EmailError)
            .HasMaxLength(500);

        builder.Entity<Invitation>().Property(invitation => invitation.ExpiresAt);

        builder.Entity<Invitation>().Property(invitation => invitation.SentAt);

        builder.Entity<Invitation>().Property(invitation => invitation.AcceptedAt);

        builder.Entity<Invitation>().Property(invitation => invitation.CancelledAt);

        builder.Entity<Invitation>().Property(invitation => invitation.CreatedAt);

        builder.Entity<Invitation>().Property(invitation => invitation.UpdatedAt);
    }
}