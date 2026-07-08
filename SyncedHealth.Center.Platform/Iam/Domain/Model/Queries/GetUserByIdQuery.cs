namespace SyncedHealth.Center.Platform.Iam.Domain.Model.Queries;

/**
 * <summary>
 *     The get all users query
 * </summary>
 * <remarks>
 *     This query object includes the user id to search
 * </remarks>
 */
/// <summary>
/// Represents a query to get user by id in the CortiSense Platform.
/// </summary>
public record GetUserByIdQuery(int Id);