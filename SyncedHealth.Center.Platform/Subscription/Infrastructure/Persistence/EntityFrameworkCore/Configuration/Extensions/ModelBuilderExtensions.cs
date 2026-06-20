using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.Subscription.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplySubscriptionConfiguration(this ModelBuilder builder)
    {
        var stringListComparer = new ValueComparer<IList<string>>(
            (left, right) => left != null && right != null && left.SequenceEqual(right),
            list => list.Aggregate(0, (hash, item) => HashCode.Combine(hash, item.GetHashCode())),
            list => list.ToList()
        );

        // Plan Aggregate
        builder.Entity<Plan>().ToTable("plans");

        builder.Entity<Plan>().HasKey(plan => plan.Id);

        builder.Entity<Plan>().Property(plan => plan.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<Plan>().Property(plan => plan.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Entity<Plan>().Property(plan => plan.Name)
            .IsRequired()
            .HasMaxLength(100);

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

        builder.Entity<Plan>().Property(plan => plan.MaxDoctors)
            .IsRequired(false);

        builder.Entity<Plan>().Property(plan => plan.MaxSupervisors)
            .IsRequired();

        builder.Entity<Plan>().Property(plan => plan.MaxTeams)
            .IsRequired();

        builder.Entity<Plan>().Property(plan => plan.MaxWorkAreas)
            .IsRequired();

        builder.Entity<Plan>().Property(plan => plan.MonthlyInvitations)
            .IsRequired();

        builder.Entity<Plan>().Property(plan => plan.DataHistoryDays)
            .IsRequired();

        builder.Entity<Plan>().Property(plan => plan.SupportLevel)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Entity<Plan>().Property(plan => plan.Recommended)
            .IsRequired();

        builder.Entity<Plan>().Property(plan => plan.FeatureKeys)
            .HasConversion(
                value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
                value => JsonSerializer.Deserialize<IList<string>>(value, (JsonSerializerOptions?)null) ?? new List<string>()
            )
            .Metadata.SetValueComparer(stringListComparer);

        builder.Entity<Plan>().Property(plan => plan.EnabledModules)
            .HasConversion(
                value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
                value => JsonSerializer.Deserialize<IList<string>>(value, (JsonSerializerOptions?)null) ?? new List<string>()
            )
            .Metadata.SetValueComparer(stringListComparer);

        builder.Entity<Plan>().Property(plan => plan.DisabledModules)
            .HasConversion(
                value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
                value => JsonSerializer.Deserialize<IList<string>>(value, (JsonSerializerOptions?)null) ?? new List<string>()
            )
            .Metadata.SetValueComparer(stringListComparer);

        builder.Entity<Plan>().Property(plan => plan.CreatedAt);

        builder.Entity<Plan>().Property(plan => plan.UpdatedAt);

        // Subscription Aggregate
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

        // CheckoutSession Aggregate
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
            .HasMaxLength(50);

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