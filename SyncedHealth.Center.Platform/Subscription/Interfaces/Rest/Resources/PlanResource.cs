namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources;

/// <summary>
/// Represents the plan resource in the CortiSense Platform.
/// </summary>
public record PlanResource
{
    public int Id { get; init; }

    public string Code { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    public decimal Price { get; init; }

    public string Currency { get; init; } = string.Empty;

    public string BillingPeriod { get; init; } = string.Empty;

    public int? MaxDoctors { get; init; }

    public int MaxSupervisors { get; init; }

    public int MaxTeams { get; init; }

    public int MaxWorkAreas { get; init; }

    public int MonthlyInvitations { get; init; }

    public int DataHistoryDays { get; init; }

    public string SupportLevel { get; init; } = string.Empty;

    public bool Recommended { get; init; }

    public IList<string> FeatureKeys { get; init; } = new List<string>();

    public IList<string> EnabledModules { get; init; } = new List<string>();

    public IList<string> DisabledModules { get; init; } = new List<string>();
}
