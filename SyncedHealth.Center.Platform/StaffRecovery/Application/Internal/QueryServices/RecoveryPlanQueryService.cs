using SyncedHealth.Center.Platform.Shared.Application.Model;
using SyncedHealth.Center.Platform.StaffRecovery.Application.QueryServices;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Errors;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Queries;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Repositories;

namespace SyncedHealth.Center.Platform.StaffRecovery.Application.Internal.QueryServices;

/// <summary>
/// Represents the recovery plan query service in the CortiSense Platform.
/// </summary>
public class RecoveryPlanQueryService(
    IRecoveryPlanRepository recoveryPlanRepository) : IRecoveryPlanQueryService
{
    public async Task<Result<IEnumerable<RecoveryPlan>>> Handle(
        GetAllRecoveryPlansQuery query,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var recoveryPlans = await recoveryPlanRepository.ListAsync(cancellationToken);
            return Result<IEnumerable<RecoveryPlan>>.Success(recoveryPlans);
        }
        catch (OperationCanceledException)
        {
            return Result<IEnumerable<RecoveryPlan>>.Failure(
                StaffRecoveryError.OperationCancelled,
                "Operation was cancelled.");
        }
        catch (Exception)
        {
            return Result<IEnumerable<RecoveryPlan>>.Failure(
                StaffRecoveryError.InternalServerError,
                "An unexpected error occurred while retrieving recovery plans.");
        }
    }

    public async Task<Result<RecoveryPlan>> Handle(
        GetRecoveryPlanByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var recoveryPlan = await recoveryPlanRepository.FindByIdAsync(query.Id, cancellationToken);

            return recoveryPlan is null
                ? Result<RecoveryPlan>.Failure(
                    StaffRecoveryError.RecoveryPlanNotFound,
                    "Recovery plan was not found.")
                : Result<RecoveryPlan>.Success(recoveryPlan);
        }
        catch (OperationCanceledException)
        {
            return Result<RecoveryPlan>.Failure(
                StaffRecoveryError.OperationCancelled,
                "Operation was cancelled.");
        }
        catch (Exception)
        {
            return Result<RecoveryPlan>.Failure(
                StaffRecoveryError.InternalServerError,
                "An unexpected error occurred while retrieving the recovery plan.");
        }
    }

    public async Task<Result<IEnumerable<RecoveryPlan>>> Handle(
        GetRecoveryPlansByMedicalStaffIdQuery query,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var recoveryPlans = await recoveryPlanRepository.FindByMedicalStaffIdAsync(
                query.MedicalStaffId,
                cancellationToken);

            return Result<IEnumerable<RecoveryPlan>>.Success(recoveryPlans);
        }
        catch (OperationCanceledException)
        {
            return Result<IEnumerable<RecoveryPlan>>.Failure(
                StaffRecoveryError.OperationCancelled,
                "Operation was cancelled.");
        }
        catch (Exception)
        {
            return Result<IEnumerable<RecoveryPlan>>.Failure(
                StaffRecoveryError.InternalServerError,
                "An unexpected error occurred while retrieving recovery plans.");
        }
    }

    public async Task<Result<IEnumerable<RecoveryPlan>>> Handle(
        GetRecoveryPlansByStatusQuery query,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var recoveryPlans = await recoveryPlanRepository.FindByStatusAsync(
                query.Status,
                cancellationToken);

            return Result<IEnumerable<RecoveryPlan>>.Success(recoveryPlans);
        }
        catch (OperationCanceledException)
        {
            return Result<IEnumerable<RecoveryPlan>>.Failure(
                StaffRecoveryError.OperationCancelled,
                "Operation was cancelled.");
        }
        catch (Exception)
        {
            return Result<IEnumerable<RecoveryPlan>>.Failure(
                StaffRecoveryError.InternalServerError,
                "An unexpected error occurred while retrieving recovery plans.");
        }
    }

    public async Task<Result<IEnumerable<RecoveryPlan>>> Handle(
        GetRecoveryPlansBySuggestedRestDaysQuery query,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var recoveryPlans = await recoveryPlanRepository.FindBySuggestedRestDaysAsync(
                query.SuggestedRestDays,
                cancellationToken);

            return Result<IEnumerable<RecoveryPlan>>.Success(recoveryPlans);
        }
        catch (OperationCanceledException)
        {
            return Result<IEnumerable<RecoveryPlan>>.Failure(
                StaffRecoveryError.OperationCancelled,
                "Operation was cancelled.");
        }
        catch (Exception)
        {
            return Result<IEnumerable<RecoveryPlan>>.Failure(
                StaffRecoveryError.InternalServerError,
                "An unexpected error occurred while retrieving recovery plans.");
        }
    }
}