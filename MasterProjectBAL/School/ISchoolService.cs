using MasterProjectCommonUtility.Response;
using MasterProjectDTOModel.school;



namespace MasterProjectBAL.School
{
    public interface ISchoolService
    {

        Task<ResultWithDataDTO<int>> AddSchool(AddSchoolRequest_DTO request_DTO);


        Task<ResultWithDataDTO<List<GetSchoolResponse_DTO>>> GetAllSchool();

    }
}
