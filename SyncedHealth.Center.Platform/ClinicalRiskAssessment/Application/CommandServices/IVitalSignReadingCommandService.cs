using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Shared.Application.Model;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.CommandServices;

/// <summary>
/// Represents the vital sign reading command service in the CortiSense Platform.
/// </summary>
public interface IVitalSignReadingCommandService
{
    Task<Result<VitalSignReading>> Handle(CreateVitalSignReadingCommand command, CancellationToken cancellationToken);
}