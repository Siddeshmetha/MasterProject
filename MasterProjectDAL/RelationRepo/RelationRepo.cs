using MasterProjectCommonUtility.Logger;
using MasterProjectDAL.DataModel;
using Microsoft.EntityFrameworkCore;

namespace MasterProjectDAL.RelationRepo
{
    public class RelationRepo : IRelationRepo
    {
        private IMasterProjectContext _masterProjectContext;
        private ILoggerManager _loggerManager;

        public RelationRepo(IMasterProjectContext masterProjectContext, ILoggerManager loggerManager)
        {
            _masterProjectContext = masterProjectContext;
            _loggerManager = loggerManager;
        }

        public async Task<Relation> AddRelation(Relation relation)
        {
           await _masterProjectContext.Relationships.AddAsync(relation);
           await _masterProjectContext.SaveChangesAsync();
            return relation;
        }

        public  async Task<List<Relation>> GetAllRelations()
        {
            return  await _masterProjectContext.Relationships.ToListAsync();
        }
    }
}
