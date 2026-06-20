namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model;

public enum ClinicalRiskAssessmentError
{
    None,
    RiskAssessmentNotFound,
    ClinicalAlertNotFound,
    VitalSignAnomalyNotFound,
    VitalSignReadingNotFound,
    InvalidRiskLevel,
    InvalidSeverity,
    InvalidStatus,
    InvalidBiometricData,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}