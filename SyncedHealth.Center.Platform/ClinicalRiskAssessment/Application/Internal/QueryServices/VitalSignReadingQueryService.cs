using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.QueryServices;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Queries;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Repositories;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.Internal.QueryServices;

public class VitalSignReadingQueryService(IVitalSignReadingRepository vitalSignReadingRepository)
    : IVitalSignReadingQueryService
{
    public async Task<IEnumerable<VitalSignReading>> Handle(
        GetAllVitalSignReadingsQuery query,
        CancellationToken cancellationToken)
    {
        return await vitalSignReadingRepository.ListAsync(cancellationToken);
    }

    public async Task<VitalSignReading?> Handle(
        GetVitalSignReadingByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await vitalSignReadingRepository.FindByIdAsync(query.Id, cancellationToken);
    }

    public async Task<IEnumerable<VitalSignReading>> Handle(
        GetVitalSignReadingsByOrganizationIdQuery query,
        CancellationToken cancellationToken)
    {
        return await vitalSignReadingRepository.FindByOrganizationIdAsync(query.OrganizationId, cancellationToken);
    }

    public async Task<IEnumerable<VitalSignReading>> Handle(
        GetVitalSignReadingsByUserIdQuery query,
        CancellationToken cancellationToken)
    {
        return await vitalSignReadingRepository.FindByUserIdAsync(query.UserId, cancellationToken);
    }
}