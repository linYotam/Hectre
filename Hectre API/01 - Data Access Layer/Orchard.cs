using System;
using System.Collections.Generic;

namespace Hectre;

public partial class Orchard
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public int Block { get; set; }

    public string SubBlock { get; set; } = null!;

    public virtual ICollection<Harvest> Harvests { get; set; } = new List<Harvest>();

    public virtual ICollection<Timesheet> Timesheets { get; set; } = new List<Timesheet>();
}
