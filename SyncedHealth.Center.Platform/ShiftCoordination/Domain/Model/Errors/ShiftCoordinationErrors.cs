using SyncedHealth.Center.Platform.Shared.Domain.Model;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Errors;

/// <summary>
/// Represents the shift coordination errors in the CortiSense Platform.
/// </summary>
public static class ShiftCoordinationErrors
{
    public static readonly Error ShiftRecordNotFound =
        new("ShiftCoordination.ShiftRecordNotFound", "The specified shift record was not found.");

    public static readonly Error InvalidShiftRecordData =
        new("ShiftCoordination.InvalidShiftRecordData", "The provided shift record data is invalid.");

    public static readonly Error InvalidShiftType =
        new("ShiftCoordination.InvalidShiftType", "The provided shift type is invalid.");

    public static readonly Error InvalidShiftStatus =
        new("ShiftCoordination.InvalidShiftStatus", "The provided shift status is invalid.");

    public static readonly Error InvalidShiftSchedule =
        new("ShiftCoordination.InvalidShiftSchedule", "The shift schedule is invalid.");
}