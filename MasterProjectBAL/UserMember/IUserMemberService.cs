using MasterProjectCommonUtility.Response;
using MasterProjectDTOModel.User;
using MasterProjectDTOModel.UserMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjectBAL.UserMember
{
    public interface IUserMemberService
    {
        Task<ResultWithDataDTO<int>> AddUserMember(AddUserMemberRequest_DTO request_DTO);
        Task<ResultWithDataDTO<int>> UpdateUserMember(UpdateUserMemberRequest_DTO request_DTO);
        Task<ResultWithDataDTO<int>> DeleteUserMember(UpdateUserMemberRequest_DTO request_DTO);
        Task<ResultWithDataDTO<GetUserMemberResponse_DTO>> GetUserByIdMember(int IdUser);
        Task<ResultWithDataDTO<List<GetUserMemberResponse_DTO>>> GetUserListMember();
        Task<ResultWithDataDTO<GetmemberChildListResponseList>> GetAllUsersByMemberName(string MemberName);
       // Task<ResultWithDataDTO<List<GetmemberChildListResponseList>>> GetAllUsersByMemberName(string MemberName);

    }
}
