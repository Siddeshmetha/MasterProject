using MasterProjectDAL.DataModel;

namespace MasterProjectDAL.RelationRepo
{
    public interface IRelationRepo
    {

        Task<Relation>AddRelation(Relation relation);

        Task<List<Relation>> GetAllRelations();



    }
}
