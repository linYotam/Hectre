using System;
using System.Collections.Generic;

namespace Hectre;

public partial class Harvest
{
    public int Id { get; set; }

    public Guid OrchardId { get; set; }

    public Guid SupervisorId { get; set; }

    public Guid PickerId { get; set; }

    public DateTime PickingDate { get; set; }

    public string Type { get; set; } = null!;

    public int BinCount { get; set; }

    public decimal HourlyWageRate { get; set; }

    public int HoursWorked { get; set; }

    public string Variety { get; set; } = null!;

    public virtual Orchard Orchard { get; set; } = null!;
}
