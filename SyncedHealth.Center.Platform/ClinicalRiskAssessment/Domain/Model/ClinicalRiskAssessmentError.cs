namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model;

/// <summary>
/// Represents the clinical risk assessment error in the CortiSense Platform.
/// </summary>
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