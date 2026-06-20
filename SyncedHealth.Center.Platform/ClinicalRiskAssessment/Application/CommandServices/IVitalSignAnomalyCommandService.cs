using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Shared.Application.Model;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.CommandServices;

public interface IVitalSignAnomalyCommandService
{
    Task<Result<VitalSignAnomaly>> Handle(CreateVitalSignAnomalyCommand command, CancellationToken cancellationToken);
    Task<Result<VitalSignAnomaly>> Handle(UpdateVitalSignAnomalyStatusCommand command, CancellationToken cancellationToken);
}