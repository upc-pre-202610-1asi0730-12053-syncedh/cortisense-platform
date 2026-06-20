namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources;

/// <summary>
/// Represents a subscription plan offering.
/// </summary>
public record PlanResource
{
    /// <summary>The unique identifier of the plan.</summary>
    /// <example>1</example>
    public int Id { get; init; }

    /// <summary>The internal code or slug of the plan.</summary>
    /// <example>premium_annual</example>
    public string Code { get; init; } = string.Empty;

    /// <summary>The public display name of the plan.</summary>
    /// <example>Premium Annual</example>
    public string Name { get; init; } = string.Empty;

    /// <summary>The base price of the plan.</summary>
    /// <example>499.99</example>
    public decimal Price { get; init; }

    /// <summary>The currency code for the price.</summary>
    /// <example>USD</example>
    public string Currency { get; init; } = string.Empty;

    /// <summary>The billing cycle for this plan.</summary>
    /// <example>Annual</example>
    public string BillingPeriod { get; init; } = string.Empty;

    /// <summary>Maximum number of doctors allowed. Null means unlimited.</summary>
    /// <example>50</example>
    public int? MaxDoctors { get; init; }

    /// <summary>Maximum number of supervisors allowed.</summary>
    /// <example>10</example>
    public int MaxSupervisors { get; init; }

    /// <summary>Maximum number of teams/departments allowed.</summary>
    /// <example>5</example>
    public int MaxTeams { get; init; }

    /// <summary>Maximum number of work areas allowed.</summary>
    /// <example>20</example>
    public int MaxWorkAreas { get; init; }

    /// <summary>Monthly invitations limit for assessments.</summary>
    /// <example>1000</example>
    public int MonthlyInvitations { get; init; }

    /// <summary>Number of days data history is retained.</summary>
    /// <example>365</example>
    public int DataHistoryDays { get; init; }

    /// <summary>The level of support provided.</summary>
    /// <example>Priority</example>
    public string SupportLevel { get; init; } = string.Empty;

    /// <summary>Indicates if this plan is recommended/highlighted.</summary>
    /// <example>true</example>
    public bool Recommended { get; init; }

    /// <summary>List of feature keys included in this plan.</summary>
    /// <example>["custom_reports", "api_access"]</example>
    public IList<string> FeatureKeys { get; init; } = new List<string>();

    /// <summary>List of enabled specialized modules.</summary>
    /// <example>["hr_analytics"]</example>
    public IList<string> EnabledModules { get; init; } = new List<string>();

    /// <summary>List of disabled specialized modules.</summary>
    /// <example>["white_labeling"]</example>
    public IList<string> DisabledModules { get; init; } = new List<string>();
}
