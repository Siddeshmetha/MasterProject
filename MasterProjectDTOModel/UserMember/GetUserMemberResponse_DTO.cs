using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjectDTOModel.UserMember
{
    public class GetUserMemberResponse_DTO
    {
        public int IduserMember { get; set; }

        public int UserId { get; set; }

        public string? MemberName { get; set; }

        public string? MemerRelation { get; set; }

        public int? IsActive { get; set; }


    }
}
