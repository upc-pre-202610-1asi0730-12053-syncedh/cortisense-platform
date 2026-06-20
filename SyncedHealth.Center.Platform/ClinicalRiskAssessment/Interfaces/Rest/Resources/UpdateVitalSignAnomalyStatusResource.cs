namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Resources;

public record UpdateVitalSignAnomalyStatusResource(
    string Status,
    int? ReviewedBy
);