using MasterProjectCommonUtility.Response;
using MasterProjectDTOModel.User;


namespace MasterProjectBAL.User
{
    public interface IUserService
    {
         
        Task<ResultWithDataDTO<int>> AddUser(AddUserRequest_DTO request_DTO);
        Task<ResultWithDataDTO<int>> UpdateUser(UpdateUserRequest_DTO request_DTO);
        Task<ResultWithDataDTO<int>> DeleteUser(UpdateUserRequest_DTO request_DTO);
        Task<ResultWithDataDTO<GetUserResponse_DTO>> GetUserById(int IdUser);
        Task<ResultWithDataDTO<List<GetUserResponse_DTO>>> GetUserList();
        Task<ResultWithDataDTO<GetUserSchoolResponse_DTO>> GetSchoolUserList(int userId);
    }

}

