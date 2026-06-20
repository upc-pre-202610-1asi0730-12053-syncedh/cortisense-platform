using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;

public partial class ShiftRecord
{
    public ShiftRecord()
    {
        Type = string.Empty;
        Status = string.Empty;
    }

    public ShiftRecord(CreateShiftRecordCommand command)
    {
        OrganizationId = command.OrganizationId;
        UserId = command.UserId;
        WorkAreaId = command.WorkAreaId;
        Type = command.Type.ToUpperInvariant();
        Status = command.Status.ToUpperInvariant();
        ScheduledStart = command.ScheduledStart;
        ScheduledEnd = command.ScheduledEnd;
        CheckInAt = command.CheckInAt;
        CheckOutAt = command.CheckOutAt;
    }

    public int Id { get; set; }

    public int OrganizationId { get; set; }

    public int UserId { get; set; }

    public int WorkAreaId { get; set; }

    public string Type { get; set; }

    public string Status { get; set; }

    public DateTimeOffset ScheduledStart { get; set; }

    public DateTimeOffset ScheduledEnd { get; set; }

    public DateTimeOffset? CheckInAt { get; set; }

    public DateTimeOffset? CheckOutAt { get; set; }

    public void UpdateStatus(string status, DateTimeOffset? checkInAt, DateTimeOffset? checkOutAt)
    {
        Status = status.ToUpperInvariant();

        if (checkInAt.HasValue)
            CheckInAt = checkInAt;

        if (checkOutAt.HasValue)
            CheckOutAt = checkOutAt;
    }
}