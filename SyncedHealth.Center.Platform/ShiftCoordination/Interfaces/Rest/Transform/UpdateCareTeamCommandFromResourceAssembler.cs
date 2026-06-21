using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;
using SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Transform;

public static class UpdateCareTeamCommandFromResourceAssembler
{
    public static UpdateCareTeamCommand ToCommandFromResource(
        int id,
        UpdateCareTeamResource resource,
        CareTeam currentCareTeam)
    {
        return new UpdateCareTeamCommand(
            id,
            string.IsNullOrWhiteSpace(resource.Name) ? currentCareTeam.Name : resource.Name,
            resource.WorkAreaId ?? currentCareTeam.WorkAreaId,
            resource.SupervisorId ?? currentCareTeam.SupervisorId,
            string.IsNullOrWhiteSpace(resource.Status) ? currentCareTeam.Status : resource.Status
        );
    }
}