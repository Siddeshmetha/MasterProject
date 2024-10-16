using MasterProjectDAL.DataModel;
using MasterProjectDTOModel.User;


namespace MasterProjectDAL.SchoolRepo
{
    public interface ISchoolRepo
    {

        Task<School> AddSchool(School school);

        Task<List<School>> GetAllSchool();

        Task<School> GetSchoolByName(int schoolId);

    }
}
