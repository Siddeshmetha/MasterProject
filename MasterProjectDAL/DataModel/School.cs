using System;
using System.Collections.Generic;

namespace MasterProjectDAL.DataModel;

public partial class School
{
    public int SchoolId { get; set; }

    public string? SchoolName { get; set; }

    public string? Location { get; set; }

    public string? Schoolcol { get; set; }

    public int SuserId { get; set; }

    public virtual User Suser { get; set; } = null!;
}
