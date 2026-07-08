using SyncedHealth.Center.Platform.Iam.Application.QueryServices;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Queries;
using SyncedHealth.Center.Platform.Iam.Domain.Repositories;

namespace SyncedHealth.Center.Platform.Iam.Application.Internal.QueryServices;

/// <summary>
/// Represents the invitation query service in the CortiSense Platform.
/// </summary>
public class InvitationQueryService(IInvitationRepository invitationRepository)
    : IInvitationQueryService
{
    public async Task<IEnumerable<Invitation>> Handle(
        GetAllInvitationsQuery query,
        CancellationToken cancellationToken)
    {
        return await invitationRepository.ListAsync(cancellationToken);
    }

    public async Task<Invitation?> Handle(
        GetInvitationByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await invitationRepository.FindByIdAsync(query.Id, cancellationToken);
    }

    public async Task<Invitation?> Handle(
        GetInvitationByTokenQuery query,
        CancellationToken cancellationToken)
    {
        return await invitationRepository.FindByTokenAsync(query.Token, cancellationToken);
    }

    public async Task<IEnumerable<Invitation>> Handle(
        GetInvitationsByOrganizationIdQuery query,
        CancellationToken cancellationToken)
    {
        return await invitationRepository.FindByOrganizationIdAsync(
            query.OrganizationId,
            cancellationToken);
    }

    public async Task<IEnumerable<Invitation>> Handle(
        GetInvitationsByEmailQuery query,
        CancellationToken cancellationToken)
    {
        return await invitationRepository.FindByEmailAsync(
            query.Email,
            cancellationToken);
    }
}