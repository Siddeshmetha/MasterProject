using MasterProjectCommonUtility.Logger;
using MasterProjectDAL.DataModel;
using MasterProjectDTOModel.User;
using Microsoft.EntityFrameworkCore;


namespace MasterProjectDAL.SchoolRepo
{
    public class SchoolRepo : ISchoolRepo
    {
        private IMasterProjectContext _masterProjectContext;
        private ILoggerManager _loggerManager;

        public SchoolRepo(IMasterProjectContext masterProjectContext, ILoggerManager loggerManager)
        {
            _masterProjectContext = masterProjectContext;
            _loggerManager = loggerManager;
        }
        public async Task<School> AddSchool(School school)
        {
            _loggerManager.LogInfo("Entry SchoolRepository => AddSchool");
            await _masterProjectContext.School.AddAsync(school);
            await _masterProjectContext.SaveChangesAsync();
            _loggerManager.LogInfo("Exit SchoolRepository=> AddSchool");
            return school;
        }

        public async Task<List<School>> GetAllSchool()
        {
           return await _masterProjectContext.School.ToListAsync();
        }

        //public async Task<List<School>> GetSchoolByName(string name)
        //{
        //   return await _masterProjectContext.School.Where(s=> s.SchoolName == name).ToListAsync();
        //}

        public async Task<School> GetSchoolByName(int userId)
        {
            return await _masterProjectContext.School.FirstOrDefaultAsync(um => um.SuserId == userId); ;
        }
    }
}
