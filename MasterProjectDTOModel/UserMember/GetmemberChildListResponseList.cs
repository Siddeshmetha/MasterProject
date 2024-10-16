using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjectDTOModel.UserMember
{
    public  class GetmemberChildListResponseList
    {
        public Userss Usre { get; set; }
        public List<GetUserMemberList> GetUserMemberLists { get; set; }
    }
    public class Userss
    {
        public string? UserName { get; set; }

        public string? Password { get; set; }
    }

    public class GetUserMemberList
    {
        public string? MemberName { get; set; }

        public string? MemberRelation { get; set; }

    }
}
