using System;
using System.Collections.Generic;

namespace MasterProjectDAL.DataModel;

public partial class UserMember
{
    public int IduserMember { get; set; }

    public int UserId { get; set; }

    public string? MemberName { get; set; }

    public string? MemerRelation { get; set; }

    public int? IsActive { get; set; }

    public int? RelationId { get; set; }

    public int? Age { get; set; }

    public virtual Relation? Relation { get; set; }

    public virtual User User { get; set; } = null!;
}
