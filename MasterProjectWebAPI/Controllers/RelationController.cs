using MasterProjectBAL.Relation;
using MasterProjectBAL.UserMember;
using MasterProjectCommonUtility.Logger;
using MasterProjectCommonUtility.Response;
using MasterProjectDTOModel.Relation;

using Microsoft.AspNetCore.Mvc;

namespace MasterProjectWebAPI.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class RelationController :ControllerBase
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IRelationService _relationService;
        public RelationController(ILoggerManager loggerManager, IRelationService relationService)
        {
            _loggerManager = loggerManager;
            _relationService = relationService;
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddRelation([FromBody] AddRelationRequest_DTO request_DTO)
        {
            ResultWithDataDTO<int> resultWithDataDTO =
                new ResultWithDataDTO<int> { IsSuccessful = false };
            if (request_DTO == null)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,Relation Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry RelationController => AddRelation");
            resultWithDataDTO = await _relationService.AddRelation(request_DTO);
            _loggerManager.LogInfo("Exit RelationController => AddRelation");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetRelationList()
        {
            ResultWithDataDTO<List<GetRelationResponse_DTO>> resultWithDataDTO =
                new ResultWithDataDTO<List<GetRelationResponse_DTO>> { IsSuccessful = false };
            _loggerManager.LogInfo("Entry RelationController => GetRelationList");
            resultWithDataDTO = await _relationService.GetAllRelation();
            _loggerManager.LogInfo("Exit RelationController => GetRelationList");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }


    }
}
