using System;
using System.Collections.Generic;

namespace Hectre;

public partial class Timesheet
{
    public int Id { get; set; }

    public Guid OrchardId { get; set; }

    public Guid SupervisorId { get; set; }

    public Guid PickerId { get; set; }

    public string? StartTime { get; set; }

    public string? EndTime { get; set; }

    public string Activity { get; set; } = null!;

    public virtual Orchard Orchard { get; set; } = null!;
}
