using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;

public partial class Specialty
{
    public Specialty()
    {
        Name = string.Empty;
    }

    public Specialty(CreateSpecialtyCommand command)
    {
        Name = command.Name.Trim();
    }

    public int Id { get; set; }

    public string Name { get; set; }
}