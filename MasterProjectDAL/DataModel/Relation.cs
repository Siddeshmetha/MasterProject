

namespace MasterProjectDAL.DataModel;

public partial class Relation
{
    public int Idrelations { get; set; }

    public string? Relation1 { get; set; }

    public virtual ICollection<UserMember> UserMember { get; set; } = new List<UserMember>();
}
