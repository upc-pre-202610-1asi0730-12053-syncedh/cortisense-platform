using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Queries;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.QueryServices;

public interface IRiskAssessmentQueryService
{
    Task<IEnumerable<RiskAssessment>> Handle(GetAllRiskAssessmentsQuery query, CancellationToken cancellationToken);
    Task<RiskAssessment?> Handle(GetRiskAssessmentByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<RiskAssessment>> Handle(GetRiskAssessmentsByOrganizationIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<RiskAssessment>> Handle(GetRiskAssessmentsByUserIdQuery query, CancellationToken cancellationToken);
}