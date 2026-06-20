using SyncedHealth.Center.Platform.Shared.Domain.Model;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Errors;

public static class ClinicalRiskAssessmentErrors
{
    public static readonly Error RiskAssessmentNotFound =
        new("ClinicalRiskAssessment.RiskAssessmentNotFound", "The specified risk assessment was not found.");

    public static readonly Error ClinicalAlertNotFound =
        new("ClinicalRiskAssessment.ClinicalAlertNotFound", "The specified clinical alert was not found.");

    public static readonly Error VitalSignAnomalyNotFound =
        new("ClinicalRiskAssessment.VitalSignAnomalyNotFound", "The specified vital sign anomaly was not found.");

    public static readonly Error VitalSignReadingNotFound =
        new("ClinicalRiskAssessment.VitalSignReadingNotFound", "The specified vital sign reading was not found.");

    public static readonly Error InvalidBiometricData =
        new("ClinicalRiskAssessment.InvalidBiometricData", "The provided biometric data is invalid.");
}