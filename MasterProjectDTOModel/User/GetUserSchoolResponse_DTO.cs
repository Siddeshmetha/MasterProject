using MasterProjectDTOModel.UserMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjectDTOModel.User
{
    public class GetUserSchoolResponse_DTO
    {
        public UserSchool? userSchool {  get; set; }
        public List<GetUserSchoolList>? getUserSchoolList { get; set; }
    }
    public  class UserSchool
    {
        public int? UserId { get; set; }
 
    }

    public class GetUserSchoolList
    {
        public string? SchoolName { get; set; }

        public string? Location { get; set; }

        public string? Schoolcol { get; set; }
    }

}
