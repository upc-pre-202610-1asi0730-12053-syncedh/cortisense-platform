$basePath = "C:\Users\marsr\cortisense-platform\SyncedHealth.Center.Platform"

# Directorios Subscription
$dirs = @(
    "$basePath\Subscription\Domain\Model\Aggregates",
    "$basePath\Subscription\Domain\Model\Entities",
    "$basePath\Subscription\Domain\Model\ValueObjects",
    "$basePath\Subscription\Domain\Repositories",
    "$basePath\Subscription\Domain\Services",
    "$basePath\Subscription\Application\Internal\CommandServices",
    "$basePath\Subscription\Application\Internal\QueryServices",
    "$basePath\Subscription\Infrastructure\Persistence\EntityFrameworkCore\Repositories",
    "$basePath\Subscription\Infrastructure\Persistence\EntityFrameworkCore\Configuration",
    "$basePath\Subscription\Interfaces\REST",
    "$basePath\Subscription\Interfaces\REST\Resources",
    "$basePath\Subscription\Interfaces\REST\Transform"
)

foreach ($dir in $dirs) {
    if (-not (Test-Path $dir)) {
        New-Item -ItemType Directory -Force -Path $dir | Out-Null
    }
}

# Archivos iniciales
Set-Content -Path "$basePath\Subscription\Domain\Model\ValueObjects\ESubscriptionStatus.cs" -Value @"
namespace SyncedHealth.Center.Platform.Subscription.Domain.Model.ValueObjects;

public enum ESubscriptionStatus
{
    Pending,
    Active,
    Expired
}
"@

Set-Content -Path "$basePath\Subscription\Domain\Model\Aggregates\SubscriptionPlan.cs" -Value @"
namespace SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;

public class SubscriptionPlan
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int MaxUsers { get; set; }
}
"@

Set-Content -Path "$basePath\Subscription\Domain\Model\Aggregates\Subscription.cs" -Value @"
using SyncedHealth.Center.Platform.Subscription.Domain.Model.ValueObjects;

namespace SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;

public class SubscriptionAgg
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SubscriptionPlanId { get; set; }
    public SubscriptionPlan? Plan { get; set; }
    public ESubscriptionStatus Status { get; set; }
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
}
"@

Set-Content -Path "$basePath\Subscription\Infrastructure\Persistence\EntityFrameworkCore\Configuration\SubscriptionModelBuilderExtensions.cs" -Value @"
using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.Subscription.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public static class SubscriptionModelBuilderExtensions
{
    public static void ConfigureSubscriptionContext(this ModelBuilder builder)
    {
        builder.Entity<SubscriptionPlan>().ToTable(`"SubscriptionPlans`");
        builder.Entity<SubscriptionPlan>().HasKey(p => p.Id);
        builder.Entity<SubscriptionPlan>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<SubscriptionPlan>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Entity<SubscriptionPlan>().Property(p => p.Price).IsRequired();

        builder.Entity<SubscriptionAgg>().ToTable(`"Subscriptions`");
        builder.Entity<SubscriptionAgg>().HasKey(s => s.Id);
        builder.Entity<SubscriptionAgg>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<SubscriptionAgg>().Property(s => s.Status).IsRequired();
        
        builder.Entity<SubscriptionAgg>()
            .HasOne(s => s.Plan)
            .WithMany()
            .HasForeignKey(s => s.SubscriptionPlanId);
    }
}
"@

Write-Host "Subscription Structure generated."
