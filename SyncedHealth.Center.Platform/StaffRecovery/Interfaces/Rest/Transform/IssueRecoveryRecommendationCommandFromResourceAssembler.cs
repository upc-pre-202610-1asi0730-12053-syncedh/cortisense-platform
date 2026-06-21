using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Commands;
using SyncedHealth.Center.Platform.StaffRecovery.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.StaffRecovery.Interfaces.Rest.Transform;

public static class IssueRecoveryRecommendationCommandFromResourceAssembler
{
    public static IssueRecoveryRecommendationCommand ToCommandFromResource(
        IssueRecoveryRecommendationResource resource)
    {
        return new IssueRecoveryRecommendationCommand(
            resource.MedicalStaffId,
            resource.Description,
            resource.SuggestedRestDays
        );
    }
}