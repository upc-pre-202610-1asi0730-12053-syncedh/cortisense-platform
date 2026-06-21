using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SyncedHealth.Center.Platform.Shared.Application.Model;
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.Shared.Resources;
using SyncedHealth.Center.Platform.Shared.Resources.Errors;
using SyncedHealth.Center.Platform.Subscription.Application.CommandServices;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Subscription.Domain.Repositories;

namespace SyncedHealth.Center.Platform.Subscription.Application.Internal.CommandServices;

public class SubscriptionCommandService(
    ISubscriptionRepository subscriptionRepository,
    IPlanRepository planRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer)
    : ISubscriptionCommandService
{
    private readonly IStringLocalizer<ErrorMessages> _localizer = localizer;

    public async Task<Result<Domain.Model.Aggregates.Subscription>> Handle(CreateSubscriptionCommand command, CancellationToken cancellationToken)
    {
        var plan = await planRepository.FindByIdAsync(command.PlanId, cancellationToken);
        if (plan == null)
            return Result<Domain.Model.Aggregates.Subscription>.Failure(SubscriptionError.PlanNotFound, _localizer[nameof(SubscriptionError.PlanNotFound)]);

        var subscription = new Domain.Model.Aggregates.Subscription(command);
        try
        {
            await subscriptionRepository.AddAsync(subscription, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Domain.Model.Aggregates.Subscription>.Success(subscription);
        }
        catch (OperationCanceledException)
        {
            return Result<Domain.Model.Aggregates.Subscription>.Failure(SubscriptionError.OperationCancelled, _localizer[nameof(SubscriptionError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Domain.Model.Aggregates.Subscription>.Failure(SubscriptionError.DatabaseError, _localizer[nameof(SubscriptionError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Domain.Model.Aggregates.Subscription>.Failure(SubscriptionError.InternalServerError, _localizer[nameof(SubscriptionError.InternalServerError)]);
        }
    }

    public async Task<Result<Domain.Model.Aggregates.Subscription>> Handle(UpdateSubscriptionCommand command, CancellationToken cancellationToken)
    {
        var subscription = await subscriptionRepository.FindByIdAsync(command.SubscriptionId, cancellationToken);
        if (subscription == null)
            return Result<Domain.Model.Aggregates.Subscription>.Failure(SubscriptionError.SubscriptionNotFound, _localizer[nameof(SubscriptionError.SubscriptionNotFound)]);

        var plan = await planRepository.FindByIdAsync(command.PlanId, cancellationToken);
        if (plan == null)
            return Result<Domain.Model.Aggregates.Subscription>.Failure(SubscriptionError.PlanNotFound, _localizer[nameof(SubscriptionError.PlanNotFound)]);

        subscription.UpdatePlan(command.PlanId);

        try
        {
            subscriptionRepository.Update(subscription);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Domain.Model.Aggregates.Subscription>.Success(subscription);
        }
        catch (OperationCanceledException)
        {
            return Result<Domain.Model.Aggregates.Subscription>.Failure(SubscriptionError.OperationCancelled, _localizer[nameof(SubscriptionError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Domain.Model.Aggregates.Subscription>.Failure(SubscriptionError.DatabaseError, _localizer[nameof(SubscriptionError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Domain.Model.Aggregates.Subscription>.Failure(SubscriptionError.InternalServerError, _localizer[nameof(SubscriptionError.InternalServerError)]);
        }
    }
}
