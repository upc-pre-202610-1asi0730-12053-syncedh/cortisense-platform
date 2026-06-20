using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SyncedHealth.Center.Platform.Shared.Application.Model;
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.Shared.Resources;
using SyncedHealth.Center.Platform.Shared.Resources.Errors;
using SyncedHealth.Center.Platform.Subscription.Application.CommandServices;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Subscription.Domain.Repositories;

namespace SyncedHealth.Center.Platform.Subscription.Application.Internal.CommandServices;

public class CheckoutSessionCommandService(
    ICheckoutSessionRepository checkoutSessionRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer)
    : ICheckoutSessionCommandService
{
    private readonly IStringLocalizer<ErrorMessages> _localizer = localizer;

    public async Task<Result<CheckoutSession>> Handle(CreateCheckoutSessionCommand command, CancellationToken cancellationToken)
    {
        var checkoutSession = new CheckoutSession(command);
        try
        {
            await checkoutSessionRepository.AddAsync(checkoutSession, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<CheckoutSession>.Success(checkoutSession);
        }
        catch (OperationCanceledException)
        {
            return Result<CheckoutSession>.Failure(SubscriptionError.OperationCancelled, _localizer[nameof(SubscriptionError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<CheckoutSession>.Failure(SubscriptionError.DatabaseError, _localizer[nameof(SubscriptionError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<CheckoutSession>.Failure(SubscriptionError.InternalServerError, _localizer[nameof(SubscriptionError.InternalServerError)]);
        }
    }
}
