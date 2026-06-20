using SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Transform;

public static class PlanResourceFromEntityAssembler
{
    public static PlanResource ToResourceFromEntity(Plan entity)
    {
        return new PlanResource
        {
            Id = entity.Id,
            Code = entity.Code,
            Name = entity.Name,
            Price = entity.Price,
            Currency = entity.Currency,
            BillingPeriod = entity.BillingPeriod.ToString(),
            MaxDoctors = entity.MaxDoctors,
            MaxSupervisors = entity.MaxSupervisors,
            MaxTeams = entity.MaxTeams,
            MaxWorkAreas = entity.MaxWorkAreas,
            MonthlyInvitations = entity.MonthlyInvitations,
            DataHistoryDays = entity.DataHistoryDays,
            SupportLevel = entity.SupportLevel.ToString(),
            Recommended = entity.Recommended,
            FeatureKeys = entity.FeatureKeys,
            EnabledModules = entity.EnabledModules,
            DisabledModules = entity.DisabledModules
        };
    }
}
