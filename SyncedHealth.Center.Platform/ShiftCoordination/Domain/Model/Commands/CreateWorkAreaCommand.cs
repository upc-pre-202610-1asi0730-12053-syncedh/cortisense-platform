namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

public record CreateWorkAreaCommand(
    int OrganizationId,
    string Name
);