using AutoMapper;
using MasterProjectCommonUtility.Logger;
using MasterProjectCommonUtility.Response;
using MasterProjectDAL.EmailRepo;
using MasterProjectDAL.SchoolRepo;
using MasterProjectDAL.UserRepo;
using MasterProjectDTOModel.User;
using System.Text.Json;

namespace MasterProjectBAL.User
{
 
   public class UserService : IUserService
    {
        readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IEmailRepository _emailRepository;
        private readonly ISchoolRepo _schoolRepo;
        public UserService(ILoggerManager loggerManager, IMapper mapper, IUserRepository productRepository, IEmailRepository emailRepository,ISchoolRepo schoolRepo)
        {
            _loggerManager = loggerManager;
            _mapper = mapper;
            _userRepository = productRepository;
            _emailRepository = emailRepository;
            _schoolRepo= schoolRepo;
        }


        public async Task<ResultWithDataDTO<int>> AddUser(AddUserRequest_DTO request_DTO)
        {
            _loggerManager.LogInfo("Entry UserService=> AddUser");
            ResultWithDataDTO<int> resultWithDataBO = new ResultWithDataDTO<int>
            {
                IsSuccessful = false
            };
            try
            {
                var dataResult = await _userRepository.AddUser(_mapper.Map<MasterProjectDAL.DataModel.User>(request_DTO));
                if (dataResult != null)
                {
                    resultWithDataBO.Data = _mapper.Map<int>(1);
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"User Save Successfully.";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }
                else
                {
                    resultWithDataBO.IsBusinessError = true;
                    resultWithDataBO.BusinessErrorMessage = $"Failed to Save User Data'.\nKindly retry or contact System Administrator.";
                    _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                }

            }
            catch (Exception ex)
            {
                resultWithDataBO.IsBusinessError = true;
                resultWithDataBO.BusinessErrorMessage = $"Failed to add User Detail-Error observed during User Name: '{request_DTO.UserName}''.\nKindly retry or contact System Administrator.";
                resultWithDataBO.SystemErrorMessage = $"UserService => AddUser: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message}and with request Object : {JsonSerializer.Serialize(request_DTO)}";
                _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                if (resultWithDataBO.SystemErrorMessage != null)
                {
                    _emailRepository.SendEmail(resultWithDataBO.SystemErrorMessage, $"AddUser");
                }
            }
            return resultWithDataBO;
        }

    

        public async Task<ResultWithDataDTO<int>> DeleteUser(UpdateUserRequest_DTO request_DTO)
        {
            _loggerManager.LogInfo("Entry UserService=> DeleteUser");
            ResultWithDataDTO<int> resultWithDataBO = new ResultWithDataDTO<int>
            {
                IsSuccessful = false
            };
            try
            {

                var preExistData = await _userRepository.GetUserById(request_DTO.IdUser);
                if (preExistData != null)
                {
                    var dataResult = await _userRepository.RemoveUser(_mapper.Map<MasterProjectDAL.DataModel.User>(request_DTO));
                    if (dataResult != null)
                    {
                        resultWithDataBO.Data = _mapper.Map<int>(1);
                        resultWithDataBO.IsSuccessful = true;
                        resultWithDataBO.Message = $"User Deleted Successfully.";
                        _loggerManager.LogInfo(resultWithDataBO.Message);
                    }
                    else
                    {
                        resultWithDataBO.IsBusinessError = true;
                        resultWithDataBO.BusinessErrorMessage = $"Failed to Save User Data'.\nKindly retry or contact System Administrator.";
                        _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                    }
                }
                else
                {
                    resultWithDataBO.Data = _mapper.Map<int>(0);
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"User not found.";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }

            }
            catch (Exception ex)
            {
                resultWithDataBO.IsBusinessError = true;
                resultWithDataBO.BusinessErrorMessage = $"Failed to add User Detail-Error observed during User Id: '{request_DTO.IdUser}''.\nKindly retry or contact System Administrator.";
                resultWithDataBO.SystemErrorMessage = $"UserService => DeleteUser: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message} and with request Object : {JsonSerializer.Serialize(request_DTO)}";
                _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                if (resultWithDataBO.SystemErrorMessage != null)
                {
                    _emailRepository.SendEmail(resultWithDataBO.SystemErrorMessage, $"DeleteUser");
                }
            }
            return resultWithDataBO;
        }

        public async Task<ResultWithDataDTO<GetUserResponse_DTO>> GetUserById(int UserId)
        {
            _loggerManager.LogInfo("Entry UserService=> GetUserById");
            ResultWithDataDTO<GetUserResponse_DTO> resultWithDataBO = new ResultWithDataDTO<GetUserResponse_DTO>
            {
                IsSuccessful = false
            };
            try
            {
                var dataResult = await _userRepository.GetUserById(UserId);
                if (dataResult != null)
                {
                    resultWithDataBO.Data = _mapper.Map<GetUserResponse_DTO>(dataResult);
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"User Retrieved Successfully.";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }
                else
                {
                    resultWithDataBO.IsBusinessError = true;
                    resultWithDataBO.BusinessErrorMessage = $"Failed to Save User Data'.\nKindly retry or contact System Administrator.";
                    _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                }

            }
            catch (Exception ex)
            {
                resultWithDataBO.IsBusinessError = true;
                resultWithDataBO.BusinessErrorMessage = $"Failed to add User Detail-Error observed during User Id: '{UserId}''.\nKindly retry or contact System Administrator.";
                resultWithDataBO.SystemErrorMessage = $"UserService => GetUserById: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message} and with request Id : {UserId}";
                _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                if (resultWithDataBO.SystemErrorMessage != null)
                {
                    _emailRepository.SendEmail(resultWithDataBO.SystemErrorMessage, $"GetUserById");
                }
            }
            return resultWithDataBO;
        }

        public async Task<ResultWithDataDTO<List<GetUserResponse_DTO>>> GetUserList()
        {
            _loggerManager.LogInfo("Entry UserService=> GetUserList");
            ResultWithDataDTO<List<GetUserResponse_DTO>> resultWithDataBO = new ResultWithDataDTO<List<GetUserResponse_DTO>>
            {
                IsSuccessful = false
            };
            try
            {
                var dataResult = await _userRepository.GetAllUserList();
                if (dataResult != null)
                {
                    resultWithDataBO.Data = _mapper.Map<List<GetUserResponse_DTO>>(dataResult);
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"Users Retrieved Successfully.";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }
                else
                {
                    resultWithDataBO.IsBusinessError = true;
                    resultWithDataBO.BusinessErrorMessage = $"Failed to Save User Data'.\nKindly retry or contact System Administrator.";
                    _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                }

            }
            catch (Exception ex)
            {
                resultWithDataBO.IsBusinessError = true;
                resultWithDataBO.BusinessErrorMessage = $"Failed to add User Detail-Error observed.\nKindly retry or contact System Administrator.";
                resultWithDataBO.SystemErrorMessage = $"UserService => GetUserList: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message}";
                _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                if (resultWithDataBO.SystemErrorMessage != null)
                {
                    _emailRepository.SendEmail(resultWithDataBO.SystemErrorMessage, $"GetUserList");
                }
            }
            return resultWithDataBO;
        }

        public async Task<ResultWithDataDTO<int>> UpdateUser(UpdateUserRequest_DTO request_DTO)
        {
            _loggerManager.LogInfo("Entry UserService=> UpdateUser");
            ResultWithDataDTO<int> resultWithDataBO = new ResultWithDataDTO<int>
            {
                IsSuccessful = false
            };
            try
            {
                var preExistData = await _userRepository.GetUserById(request_DTO.IdUser);
                if (preExistData != null)
                {
                    var dataResult = await _userRepository.UpdateUser(_mapper.Map<MasterProjectDAL.DataModel.User>(request_DTO));
                    if (dataResult != null)
                    {
                        resultWithDataBO.Data = _mapper.Map<int>(1);
                        resultWithDataBO.IsSuccessful = true;
                        resultWithDataBO.Message = $"User Updated Successfully.";
                        _loggerManager.LogInfo(resultWithDataBO.Message);
                    }
                    else
                    {
                        resultWithDataBO.IsBusinessError = true;
                        resultWithDataBO.BusinessErrorMessage = $"Failed to Save User Data'.\nKindly retry or contact System Administrator.";
                        _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                    }
                }
                else
                {
                    resultWithDataBO.Data = _mapper.Map<int>(0);
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"User not found.";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }

            }
            catch (Exception ex)
            {
                resultWithDataBO.IsBusinessError = true;
                resultWithDataBO.BusinessErrorMessage = $"Failed to update User Detail-Error observed during User Id: '{request_DTO.IdUser}''.\nKindly retry or contact System Administrator.";
                resultWithDataBO.SystemErrorMessage = $"UserService => UpdateUser: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message} and with request Object : {JsonSerializer.Serialize(request_DTO)}";
                _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                if (resultWithDataBO.SystemErrorMessage != null)
                {
                    _emailRepository.SendEmail(resultWithDataBO.SystemErrorMessage, $"UpdateUser");
                }
            }
            return resultWithDataBO;
        }


        public async Task<ResultWithDataDTO<GetUserSchoolResponse_DTO>> GetSchoolUserList(int  userId)
        {
            _loggerManager.LogInfo("Entry UserService=> GetAllSchoolByUserName");
            ResultWithDataDTO<GetUserSchoolResponse_DTO> resultWithDataBO = new ResultWithDataDTO<GetUserSchoolResponse_DTO>
            {
                IsSuccessful = false
            };

            try
            {
                GetUserSchoolResponse_DTO dataResult = new GetUserSchoolResponse_DTO();
                
                var userData = await _userRepository.GetUserById(userId);
                if (userData != null)
                {
                    dataResult.userSchool = _mapper.Map<UserSchool>(userData);

                    var memberData = await _schoolRepo.GetSchoolByName(userData.IdUser);
                    if (memberData!=null)
                    {
                        dataResult.getUserSchoolList = _mapper.Map<List<GetUserSchoolList>>(memberData);


                        resultWithDataBO.Data = dataResult;
                        resultWithDataBO.IsSuccessful = true;
                        resultWithDataBO.Message = $"User Member not found";
                        _loggerManager.LogInfo(resultWithDataBO.Message);
                    }
                    else
                    {

                    }
                }
                else
                {
                    resultWithDataBO.Data = null;
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"User Member not found";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }

                //var dataResult = await _userMemberRepository.GetAllUsersByMemberName(MemberName);
                //if (dataResult != null)
                //{
                //    resultWithDataBO.Data = _mapper.Map<List<GetmemberChildListResponseList>>(dataResult);
                //    resultWithDataBO.IsSuccessful = true;
                //    resultWithDataBO.Message = $"UserMember Retrieved Successfully.";
                //    _loggerManager.LogInfo(resultWithDataBO.Message);
                //}
                //else
                //{
                //    resultWithDataBO.IsBusinessError = true;
                //    resultWithDataBO.BusinessErrorMessage = $"Failed to Save UserMember Data'.\nKindly retry or contact System Administrator.";
                //    _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                //}

            }
            catch (Exception ex)
            {
                resultWithDataBO.IsBusinessError = true;
                resultWithDataBO.BusinessErrorMessage = $"Failed to add UserMember Detail-Error observed during UserMember Id: '{userId}''.\nKindly retry or contact System Administrator.";
                resultWithDataBO.SystemErrorMessage = $"UserMemberService => GetAllUsersByMemberName: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message} and with request Id : {userId}";
                _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                if (resultWithDataBO.SystemErrorMessage != null)
                {
                    _emailRepository.SendEmail(resultWithDataBO.SystemErrorMessage, $"GetAllUsersByMemberName");
                }
            }
            return resultWithDataBO;
        }


    }
}
