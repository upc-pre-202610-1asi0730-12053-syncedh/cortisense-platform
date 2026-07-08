using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Shared.Application.Model;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.CommandServices;

/// <summary>
/// Represents the risk assessment command service in the CortiSense Platform.
/// </summary>
public interface IRiskAssessmentCommandService
{
    Task<Result<RiskAssessment>> Handle(CreateRiskAssessmentCommand command, CancellationToken cancellationToken);
}