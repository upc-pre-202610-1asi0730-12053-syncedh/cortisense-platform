using SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Queries;

namespace SyncedHealth.Center.Platform.Iam.Application.QueryServices;

/// <summary>
/// Represents the invitation query service in the CortiSense Platform.
/// </summary>
public interface IInvitationQueryService
{
    Task<IEnumerable<Invitation>> Handle(
        GetAllInvitationsQuery query,
        CancellationToken cancellationToken
    );

    Task<Invitation?> Handle(
        GetInvitationByIdQuery query,
        CancellationToken cancellationToken
    );

    Task<Invitation?> Handle(
        GetInvitationByTokenQuery query,
        CancellationToken cancellationToken
    );

    Task<IEnumerable<Invitation>> Handle(
        GetInvitationsByOrganizationIdQuery query,
        CancellationToken cancellationToken
    );

    Task<IEnumerable<Invitation>> Handle(
        GetInvitationsByEmailQuery query,
        CancellationToken cancellationToken
    );
}