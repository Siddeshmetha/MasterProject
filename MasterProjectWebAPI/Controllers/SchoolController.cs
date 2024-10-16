using MasterProjectBAL.School; 
using MasterProjectCommonUtility.Logger;
using MasterProjectCommonUtility.Response;
using MasterProjectDTOModel.school;
using Microsoft.AspNetCore.Mvc;

namespace MasterProjectWebAPI.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class SchoolController:ControllerBase
    {
        private readonly ILoggerManager _loggerManager;
        private readonly ISchoolService _schoolService;
        public SchoolController(ILoggerManager loggerManager, ISchoolService schoolService)
        {
            _loggerManager = loggerManager;
            _schoolService = schoolService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddSchool([FromBody] AddSchoolRequest_DTO request_DTO)
        {
            ResultWithDataDTO<int> resultWithDataDTO =
                new ResultWithDataDTO<int> { IsSuccessful = false };
            if (request_DTO == null)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,School Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry SchoolController => AddSchool");
            resultWithDataDTO = await _schoolService.AddSchool(request_DTO);
            _loggerManager.LogInfo("Exit SchoolController => AddSchool");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetSchoolList()
        {
            ResultWithDataDTO<List<GetSchoolResponse_DTO>> resultWithDataDTO =
                new ResultWithDataDTO<List<GetSchoolResponse_DTO>> { IsSuccessful = false };
            _loggerManager.LogInfo("Entry SchoolController=> GetSchoolList");
            resultWithDataDTO = await _schoolService.GetAllSchool();
            _loggerManager.LogInfo("Exit SchoolController=> GetSchoolList");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }
    }
}
