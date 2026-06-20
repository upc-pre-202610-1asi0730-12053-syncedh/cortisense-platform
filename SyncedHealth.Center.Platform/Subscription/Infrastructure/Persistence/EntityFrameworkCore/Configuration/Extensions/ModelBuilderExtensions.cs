using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.Subscription.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplySubscriptionConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Plan>().ToTable("plans");
        builder.Entity<Plan>().HasKey(plan => plan.Id);

        builder.Entity<Plan>().Property(plan => plan.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<Plan>().Property(plan => plan.Code)
            .IsRequired()
            .HasMaxLength(40);

        builder.Entity<Plan>().Property(plan => plan.Name)
            .IsRequired()
            .HasMaxLength(80);

        builder.Entity<Plan>().Property(plan => plan.Price)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Entity<Plan>().Property(plan => plan.Currency)
            .IsRequired()
            .HasMaxLength(10);

        builder.Entity<Plan>().Property(plan => plan.BillingPeriod)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Entity<Plan>().Property(plan => plan.MaxDoctors);
        builder.Entity<Plan>().Property(plan => plan.MaxSupervisors);
        builder.Entity<Plan>().Property(plan => plan.MaxTeams);
        builder.Entity<Plan>().Property(plan => plan.MaxWorkAreas);
        builder.Entity<Plan>().Property(plan => plan.MonthlyInvitations);
        builder.Entity<Plan>().Property(plan => plan.DataHistoryDays);

        builder.Entity<Plan>().Property(plan => plan.SupportLevel)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Entity<Plan>().Property(plan => plan.Recommended)
            .IsRequired();

        builder.Entity<Plan>().Property(plan => plan.FeatureKeys)
            .HasColumnType("json");

        builder.Entity<Plan>().Property(plan => plan.EnabledModules)
            .HasColumnType("json");

        builder.Entity<Plan>().Property(plan => plan.DisabledModules)
            .HasColumnType("json");

        builder.Entity<Plan>().Property(plan => plan.CreatedAt);
        builder.Entity<Plan>().Property(plan => plan.UpdatedAt);

        builder.Entity<Domain.Model.Aggregates.Subscription>().ToTable("subscriptions");
        builder.Entity<Domain.Model.Aggregates.Subscription>().HasKey(subscription => subscription.Id);

        builder.Entity<Domain.Model.Aggregates.Subscription>().Property(subscription => subscription.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<Domain.Model.Aggregates.Subscription>().Property(subscription => subscription.OrganizationId)
            .IsRequired();

        builder.Entity<Domain.Model.Aggregates.Subscription>().Property(subscription => subscription.PlanId)
            .IsRequired();

        builder.Entity<Domain.Model.Aggregates.Subscription>().Property(subscription => subscription.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Entity<Domain.Model.Aggregates.Subscription>().Property(subscription => subscription.StartedAt)
            .IsRequired();

        builder.Entity<Domain.Model.Aggregates.Subscription>().Property(subscription => subscription.CancelledAt);

        builder.Entity<Domain.Model.Aggregates.Subscription>().Property(subscription => subscription.StripeSubscriptionId)
            .HasMaxLength(120);

        builder.Entity<Domain.Model.Aggregates.Subscription>().Property(subscription => subscription.StripeCustomerId)
            .HasMaxLength(120);

        builder.Entity<Domain.Model.Aggregates.Subscription>().Property(subscription => subscription.CreatedAt);
        builder.Entity<Domain.Model.Aggregates.Subscription>().Property(subscription => subscription.UpdatedAt);

        builder.Entity<CheckoutSession>().ToTable("checkout_sessions");
        builder.Entity<CheckoutSession>().HasKey(checkoutSession => checkoutSession.Id);

        builder.Entity<CheckoutSession>().Property(checkoutSession => checkoutSession.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<CheckoutSession>().Property(checkoutSession => checkoutSession.OrganizationId)
            .IsRequired();

        builder.Entity<CheckoutSession>().Property(checkoutSession => checkoutSession.AdministratorId)
            .IsRequired();

        builder.Entity<CheckoutSession>().Property(checkoutSession => checkoutSession.SubscriptionId)
            .IsRequired();

        builder.Entity<CheckoutSession>().Property(checkoutSession => checkoutSession.PlanId)
            .IsRequired();

        builder.Entity<CheckoutSession>().Property(checkoutSession => checkoutSession.PlanCode)
            .IsRequired()
            .HasMaxLength(40);

        builder.Entity<CheckoutSession>().Property(checkoutSession => checkoutSession.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Entity<CheckoutSession>().Property(checkoutSession => checkoutSession.StripeSessionId)
            .HasMaxLength(120);

        builder.Entity<CheckoutSession>().Property(checkoutSession => checkoutSession.StripeUrl)
            .HasMaxLength(600);

        builder.Entity<CheckoutSession>().Property(checkoutSession => checkoutSession.StripeSubscriptionId)
            .HasMaxLength(120);

        builder.Entity<CheckoutSession>().Property(checkoutSession => checkoutSession.StripeCustomerId)
            .HasMaxLength(120);

        builder.Entity<CheckoutSession>().Property(checkoutSession => checkoutSession.CompletedAt);
        builder.Entity<CheckoutSession>().Property(checkoutSession => checkoutSession.FailedAt);
        builder.Entity<CheckoutSession>().Property(checkoutSession => checkoutSession.CancelledAt);

        builder.Entity<CheckoutSession>().Property(checkoutSession => checkoutSession.ErrorMessage)
            .HasMaxLength(500);

        builder.Entity<CheckoutSession>().Property(checkoutSession => checkoutSession.CreatedAt);
        builder.Entity<CheckoutSession>().Property(checkoutSession => checkoutSession.UpdatedAt);
    }
}