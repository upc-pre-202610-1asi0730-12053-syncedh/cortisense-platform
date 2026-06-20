namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Commands;

public record UpdateClinicalAlertStatusCommand(
    int Id,
    string Status,
    int? ResolvedBy
);