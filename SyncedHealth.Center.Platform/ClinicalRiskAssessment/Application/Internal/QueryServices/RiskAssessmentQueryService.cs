using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.QueryServices;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Queries;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Repositories;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.Internal.QueryServices;

public class RiskAssessmentQueryService(IRiskAssessmentRepository riskAssessmentRepository)
    : IRiskAssessmentQueryService
{
    public async Task<IEnumerable<RiskAssessment>> Handle(
        GetAllRiskAssessmentsQuery query,
        CancellationToken cancellationToken)
    {
        return await riskAssessmentRepository.ListAsync(cancellationToken);
    }

    public async Task<RiskAssessment?> Handle(
        GetRiskAssessmentByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await riskAssessmentRepository.FindByIdAsync(query.Id, cancellationToken);
    }

    public async Task<IEnumerable<RiskAssessment>> Handle(
        GetRiskAssessmentsByOrganizationIdQuery query,
        CancellationToken cancellationToken)
    {
        return await riskAssessmentRepository.FindByOrganizationIdAsync(query.OrganizationId, cancellationToken);
    }

    public async Task<IEnumerable<RiskAssessment>> Handle(
        GetRiskAssessmentsByUserIdQuery query,
        CancellationToken cancellationToken)
    {
        return await riskAssessmentRepository.FindByUserIdAsync(query.UserId, cancellationToken);
    }
}