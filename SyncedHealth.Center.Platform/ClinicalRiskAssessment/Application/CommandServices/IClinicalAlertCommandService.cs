using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Shared.Application.Model;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.CommandServices;

public interface IClinicalAlertCommandService
{
    Task<Result<ClinicalAlert>> Handle(CreateClinicalAlertCommand command, CancellationToken cancellationToken);
    Task<Result<ClinicalAlert>> Handle(UpdateClinicalAlertStatusCommand command, CancellationToken cancellationToken);
}