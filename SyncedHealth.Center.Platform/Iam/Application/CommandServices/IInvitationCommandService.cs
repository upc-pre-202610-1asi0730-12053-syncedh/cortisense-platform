using SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Shared.Application.Model;

namespace SyncedHealth.Center.Platform.Iam.Application.CommandServices;

public interface IInvitationCommandService
{
    Task<Result<Invitation>> Handle(
        CreateInvitationCommand command,
        CancellationToken cancellationToken
    );

    Task<Result<Invitation>> Handle(
        UpdateInvitationCommand command,
        CancellationToken cancellationToken
    );

    Task<Result<Invitation>> Handle(
        DeleteInvitationCommand command,
        CancellationToken cancellationToken
    );

    Task<Result<Invitation>> Handle(
        AcceptInvitationCommand command,
        CancellationToken cancellationToken
    );
}