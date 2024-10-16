using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjectDTOModel.User
{
    public class AddUserRequest_DTO
    {
        public int? IdUser { get; set; }
        public string? UserName { get; set; }
        
        public string? Password { get; set; }

        public int? IaActive { get; set; }
    }




}
