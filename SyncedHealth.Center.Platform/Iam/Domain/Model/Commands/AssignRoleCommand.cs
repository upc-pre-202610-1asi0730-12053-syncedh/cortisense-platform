namespace SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;

/// <summary>
/// Command to assign role.
/// </summary>
public record AssignRoleCommand(int UserId, string Role);
