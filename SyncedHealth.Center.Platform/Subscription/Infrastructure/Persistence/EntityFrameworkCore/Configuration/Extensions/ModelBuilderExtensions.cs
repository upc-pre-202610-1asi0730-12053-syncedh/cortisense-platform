using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.Subscription.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplySubscriptionConfiguration(this ModelBuilder builder)
    {
        // Plan Aggregate
        builder.Entity<Plan>().HasKey(p => p.Id);
        builder.Entity<Plan>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Plan>().Property(p => p.Code).IsRequired().HasMaxLength(50);
        builder.Entity<Plan>().Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Plan>().Property(p => p.Price).IsRequired().HasPrecision(10, 2);
        builder.Entity<Plan>().Property(p => p.Currency).IsRequired().HasMaxLength(10);
        builder.Entity<Plan>().Property(p => p.BillingPeriod).IsRequired().HasConversion<string>().HasMaxLength(20);
        builder.Entity<Plan>().Property(p => p.MaxDoctors).IsRequired(false);
        builder.Entity<Plan>().Property(p => p.MaxSupervisors).IsRequired();
        builder.Entity<Plan>().Property(p => p.MaxTeams).IsRequired();
        builder.Entity<Plan>().Property(p => p.MaxWorkAreas).IsRequired();
        builder.Entity<Plan>().Property(p => p.MonthlyInvitations).IsRequired();
        builder.Entity<Plan>().Property(p => p.DataHistoryDays).IsRequired();
        builder.Entity<Plan>().Property(p => p.SupportLevel).IsRequired().HasConversion<string>().HasMaxLength(30);
        builder.Entity<Plan>().Property(p => p.Recommended).IsRequired();
        
        // JSON conversions for lists
        builder.Entity<Plan>().Property(p => p.FeatureKeys).HasConversion(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null!),
            v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null!) ?? new List<string>()
        );
        builder.Entity<Plan>().Property(p => p.EnabledModules).HasConversion(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null!),
            v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null!) ?? new List<string>()
        );
        builder.Entity<Plan>().Property(p => p.DisabledModules).HasConversion(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null!),
            v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null!) ?? new List<string>()
        );

        // Subscription Aggregate
        builder.Entity<Domain.Model.Aggregates.Subscription>().HasKey(s => s.Id);
        builder.Entity<Domain.Model.Aggregates.Subscription>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Domain.Model.Aggregates.Subscription>().Property(s => s.OrganizationId).IsRequired();
        builder.Entity<Domain.Model.Aggregates.Subscription>().Property(s => s.PlanId).IsRequired();
        builder.Entity<Domain.Model.Aggregates.Subscription>().Property(s => s.Status).IsRequired().HasConversion<string>().HasMaxLength(30);
        builder.Entity<Domain.Model.Aggregates.Subscription>().Property(s => s.StartedAt).IsRequired();

        // CheckoutSession Aggregate
        builder.Entity<CheckoutSession>().HasKey(c => c.Id);
        builder.Entity<CheckoutSession>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<CheckoutSession>().Property(c => c.OrganizationId).IsRequired();
        builder.Entity<CheckoutSession>().Property(c => c.AdministratorId).IsRequired();
        builder.Entity<CheckoutSession>().Property(c => c.SubscriptionId).IsRequired();
        builder.Entity<CheckoutSession>().Property(c => c.PlanId).IsRequired();
        builder.Entity<CheckoutSession>().Property(c => c.PlanCode).IsRequired().HasMaxLength(50);
        builder.Entity<CheckoutSession>().Property(c => c.Status).IsRequired().HasConversion<string>().HasMaxLength(30);
    }
}
