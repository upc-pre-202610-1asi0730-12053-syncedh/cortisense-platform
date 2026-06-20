namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Commands;

public record UpdateVitalSignAnomalyStatusCommand(
    int Id,
    string Status,
    int? ReviewedBy
);