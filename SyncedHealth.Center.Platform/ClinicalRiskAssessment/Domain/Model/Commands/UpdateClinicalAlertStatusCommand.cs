namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Commands;

/// <summary>
/// Command to update clinical alert status.
/// </summary>
public record UpdateClinicalAlertStatusCommand(
    int Id,
    string Status,
    int? ResolvedBy
);