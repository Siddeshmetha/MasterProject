using MasterProjectBAL.UserMember;
using MasterProjectCommonUtility.Logger;
using MasterProjectCommonUtility.Response;
using Microsoft.AspNetCore.Http;
using MasterProjectDTOModel.UserMember;
using Microsoft.AspNetCore.Mvc;

namespace MasterProjectWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserMemberController : ControllerBase
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IUserMemberService _userServiceMember;
        public UserMemberController(ILoggerManager loggerManager, IUserMemberService userServiceMember)
        {
            _loggerManager = loggerManager;
            _userServiceMember = userServiceMember;
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddUserMember([FromBody] AddUserMemberRequest_DTO request_DTO)
        {
            ResultWithDataDTO<int> resultWithDataDTO =
                new ResultWithDataDTO<int> { IsSuccessful = false };
            if (request_DTO == null)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,UserMember Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry UserMemberController=> AddUserMember");
            resultWithDataDTO = await _userServiceMember.AddUserMember(request_DTO);
            _loggerManager.LogInfo("Exit UserMemberController=> AddUserMember");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateUserMember([FromBody] UpdateUserMemberRequest_DTO request_DTO)
        {
            ResultWithDataDTO<int> resultWithDataDTO =
                new ResultWithDataDTO<int> { IsSuccessful = false };
            if (request_DTO == null)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,UserMember Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry UserMemberController=> UpdateUserMember");
            resultWithDataDTO = await _userServiceMember.UpdateUserMember(request_DTO);
            _loggerManager.LogInfo("Exit UserMemberController=> UpdateUserMember");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetUserMemberList()
        {
            ResultWithDataDTO<List<GetUserMemberResponse_DTO>> resultWithDataDTO =
                new ResultWithDataDTO<List<GetUserMemberResponse_DTO>> { IsSuccessful = false };
            _loggerManager.LogInfo("Entry UserMemberController=> GetUserList");
            resultWithDataDTO = await _userServiceMember.GetUserListMember();
            _loggerManager.LogInfo("Exit UserMemberController=> GetUserList");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }


        [HttpGet]
        [Route("[action]/{UserId}")]
        public async Task<IActionResult> GetUserMemberById(int UserId)
        {
            ResultWithDataDTO<GetUserMemberResponse_DTO> resultWithDataDTO =
                new ResultWithDataDTO<GetUserMemberResponse_DTO> { IsSuccessful = false };
            if (UserId == 0)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,UserMember Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry UserMemberController=> GetUserMemberById");
            resultWithDataDTO = await _userServiceMember.GetUserByIdMember(UserId);
            _loggerManager.LogInfo("Exit UserMemberController=> GetUserMemberById");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> DeleteUserMember([FromBody] UpdateUserMemberRequest_DTO request_DTO)
        {
            ResultWithDataDTO<int> resultWithDataDTO =
                new ResultWithDataDTO<int> { IsSuccessful = false };
            if (request_DTO == null)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,UserMember Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry UserMemberController=> DeleteUserMember");
            resultWithDataDTO = await _userServiceMember.DeleteUserMember(request_DTO);
            _loggerManager.LogInfo("Exit UserMemberController=> DeleteUserMember");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }

        [HttpGet]
        [Route("[action]/{MemberName}")]
        public async Task<IActionResult> GetAllUsersByMemberName(string MemberName)
        {
            ResultWithDataDTO<GetmemberChildListResponseList> resultWithDataDTO =
                new ResultWithDataDTO<GetmemberChildListResponseList> { IsSuccessful = false };
            if (string.IsNullOrEmpty(MemberName))
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,UserMember Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry UserMemberController=> GetAllUsersByMemberName");
            resultWithDataDTO = await _userServiceMember.GetAllUsersByMemberName(MemberName);
            _loggerManager.LogInfo("Exit UserMemberController=> GetAllUsersByMemberName");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }


    }
}
