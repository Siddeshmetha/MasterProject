using MasterProjectDAL.DataModel;
using MasterProjectDTOModel.UserMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjectDAL.UserMemberRepo
{
    public interface IUserMemberRepository
    {
        Task<DataModel.UserMember> AddUserMember(DataModel.UserMember userMember);

        Task<DataModel.UserMember> UpdateUserMember(DataModel.UserMember userMember);

        Task<DataModel.UserMember> GetUserMemberById(int UserMemeberId);

     //   Task<List<GetmemberChildListResponseList>> GetAllUsersByMemberName(string MemberName);

        Task<List<DataModel.UserMember>> GetUserMembersList();

        Task<DataModel.UserMember> RemoveUserMember(DataModel.UserMember userMember);
        Task<List<UserMember>> GetAllUsersByMemberName(int userId);


    }
}
