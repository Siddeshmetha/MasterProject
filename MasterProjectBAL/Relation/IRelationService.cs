using MasterProjectCommonUtility.Response;
using MasterProjectDTOModel.Relation;


namespace MasterProjectBAL.Relation
{
    public interface IRelationService
    {

        Task<ResultWithDataDTO<int>> AddRelation(AddRelationRequest_DTO request_DTO);

        Task<ResultWithDataDTO<List<GetRelationResponse_DTO>>> GetAllRelation();

    }
}
