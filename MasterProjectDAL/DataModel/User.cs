using System;
using System.Collections.Generic;

namespace MasterProjectDAL.DataModel;

public partial class User
{
    public int IdUser { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public int? IaActive { get; set; }

    public virtual ICollection<School> School { get; set; } = new List<School>();

    public virtual ICollection<UserMember> UserMember { get; set; } = new List<UserMember>();
}
