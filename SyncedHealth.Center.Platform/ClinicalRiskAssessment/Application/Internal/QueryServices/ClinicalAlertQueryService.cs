using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.QueryServices;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Queries;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Repositories;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.Internal.QueryServices;

public class ClinicalAlertQueryService(IClinicalAlertRepository clinicalAlertRepository)
    : IClinicalAlertQueryService
{
    public async Task<IEnumerable<ClinicalAlert>> Handle(
        GetAllClinicalAlertsQuery query,
        CancellationToken cancellationToken)
    {
        return await clinicalAlertRepository.ListAsync(cancellationToken);
    }

    public async Task<ClinicalAlert?> Handle(
        GetClinicalAlertByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await clinicalAlertRepository.FindByIdAsync(query.Id, cancellationToken);
    }

    public async Task<IEnumerable<ClinicalAlert>> Handle(
        GetClinicalAlertsByOrganizationIdQuery query,
        CancellationToken cancellationToken)
    {
        return await clinicalAlertRepository.FindByOrganizationIdAsync(query.OrganizationId, cancellationToken);
    }

    public async Task<IEnumerable<ClinicalAlert>> Handle(
        GetClinicalAlertsByUserIdQuery query,
        CancellationToken cancellationToken)
    {
        return await clinicalAlertRepository.FindByUserIdAsync(query.UserId, cancellationToken);
    }

    public async Task<IEnumerable<ClinicalAlert>> Handle(
        GetClinicalAlertsByStatusQuery query,
        CancellationToken cancellationToken)
    {
        return await clinicalAlertRepository.FindByStatusAsync(query.Status, cancellationToken);
    }
}