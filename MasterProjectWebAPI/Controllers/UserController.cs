using MasterProjectBAL.User;
using Microsoft.AspNetCore.Http;
using MasterProjectCommonUtility.Logger;
using MasterProjectCommonUtility.Response;
using MasterProjectDTOModel.User;
using Microsoft.AspNetCore.Mvc;

namespace MasterProjectWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IUserService _userService;
        public UserController(ILoggerManager loggerManager, IUserService userService)
        {
            _loggerManager = loggerManager;
            _userService = userService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequest_DTO request_DTO)
        {
            ResultWithDataDTO<int> resultWithDataDTO =
                new ResultWithDataDTO<int> { IsSuccessful = false };
            if (request_DTO == null)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,User Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry UserController=> AddUser");
            resultWithDataDTO = await _userService.AddUser(request_DTO);
            _loggerManager.LogInfo("Exit UserController=> AddUser");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest_DTO request_DTO)
        {
            ResultWithDataDTO<int> resultWithDataDTO =
                new ResultWithDataDTO<int> { IsSuccessful = false };
            if (request_DTO == null)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,User Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry UserController=> UpdateUser");
            resultWithDataDTO = await _userService.UpdateUser(request_DTO);
            _loggerManager.LogInfo("Exit UserController=> UpdateUser");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetUserList()
        {
            ResultWithDataDTO<List<GetUserResponse_DTO>> resultWithDataDTO =
                new ResultWithDataDTO<List<GetUserResponse_DTO>> { IsSuccessful = false };
            _loggerManager.LogInfo("Entry UserController=> GetUserList");
            resultWithDataDTO = await _userService.GetUserList();
            _loggerManager.LogInfo("Exit UserController=> GetUserList");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }


        [HttpGet]
        [Route("[action]/{UserId}")]
        public async Task<IActionResult> GetUserById(int UserId)
        {
            ResultWithDataDTO<GetUserResponse_DTO> resultWithDataDTO =
                new ResultWithDataDTO<GetUserResponse_DTO> { IsSuccessful = false };
            if (UserId == 0)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,User Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry UserController=> GetUserById");
            resultWithDataDTO = await _userService.GetUserById(UserId);
            _loggerManager.LogInfo("Exit UserController=> GetUserById");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> DeleteUser([FromBody] UpdateUserRequest_DTO request_DTO)
        {
            ResultWithDataDTO<int> resultWithDataDTO =
                new ResultWithDataDTO<int> { IsSuccessful = false };
            if (request_DTO == null)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,User Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry UserController=> DeleteUser");
            resultWithDataDTO = await _userService.DeleteUser(request_DTO);
            _loggerManager.LogInfo("Exit UserController=> DeleteUser");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            //
            else { return BadRequest(resultWithDataDTO); }
        }

     




    }
}
