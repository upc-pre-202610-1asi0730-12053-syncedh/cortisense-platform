using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Queries;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.QueryServices;

public interface IClinicalAlertQueryService
{
    Task<IEnumerable<ClinicalAlert>> Handle(GetAllClinicalAlertsQuery query, CancellationToken cancellationToken);
    Task<ClinicalAlert?> Handle(GetClinicalAlertByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<ClinicalAlert>> Handle(GetClinicalAlertsByOrganizationIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<ClinicalAlert>> Handle(GetClinicalAlertsByUserIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<ClinicalAlert>> Handle(
        GetClinicalAlertsByStatusQuery query,
        CancellationToken cancellationToken);

    Task<IEnumerable<ClinicalAlert>> Handle(
        GetClinicalAlertsBySeverityQuery query,
        CancellationToken cancellationToken);
}