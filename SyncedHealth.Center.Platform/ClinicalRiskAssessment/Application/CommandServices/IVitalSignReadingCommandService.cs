using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Shared.Application.Model;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.CommandServices;

public interface IVitalSignReadingCommandService
{
    Task<Result<VitalSignReading>> Handle(CreateVitalSignReadingCommand command, CancellationToken cancellationToken);
}