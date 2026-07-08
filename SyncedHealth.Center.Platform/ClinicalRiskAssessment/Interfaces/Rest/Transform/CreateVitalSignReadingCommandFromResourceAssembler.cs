using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Commands;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Transform;

/// <summary>
/// Represents the create vital sign reading command from resource assembler in the CortiSense Platform.
/// </summary>
public static class CreateVitalSignReadingCommandFromResourceAssembler
{
    public static CreateVitalSignReadingCommand ToCommandFromResource(CreateVitalSignReadingResource resource)
    {
        return new CreateVitalSignReadingCommand(
            resource.OrganizationId,
            resource.UserId,
            resource.HeartRate,
            resource.Hrv,
            resource.FatigueLevel,
            resource.CortisolLevel,
            resource.SensorStatus,
            resource.RecordedAt
        );
    }
}