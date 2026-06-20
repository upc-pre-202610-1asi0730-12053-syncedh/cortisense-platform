using SyncedHealth.Center.Platform.Subscription.Domain.Model.ValueObjects;

namespace SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;

public partial class Plan
{
    public Plan()
    {
        Code = string.Empty;
        Name = string.Empty;
        Currency = string.Empty;
        FeatureKeys = new List<string>();
        EnabledModules = new List<string>();
        DisabledModules = new List<string>();
    }

    public int Id { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public string Currency { get; private set; }
    public EBillingPeriod BillingPeriod { get; private set; }
    public int? MaxDoctors { get; private set; }
    public int MaxSupervisors { get; private set; }
    public int MaxTeams { get; private set; }
    public int MaxWorkAreas { get; private set; }
    public int MonthlyInvitations { get; private set; }
    public int DataHistoryDays { get; private set; }
    public ESupportLevel SupportLevel { get; private set; }
    public bool Recommended { get; private set; }
    public IList<string> FeatureKeys { get; private set; }
    public IList<string> EnabledModules { get; private set; }
    public IList<string> DisabledModules { get; private set; }
}
