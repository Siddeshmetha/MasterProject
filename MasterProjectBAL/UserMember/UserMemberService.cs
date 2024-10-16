using AutoMapper;
using MasterProjectCommonUtility.Logger;
using MasterProjectCommonUtility.Response;
using MasterProjectDAL.EmailRepo;
using MasterProjectDAL.UserMemberRepo;
using MasterProjectDAL.UserRepo;
using MasterProjectDTOModel.UserMember;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;


namespace MasterProjectBAL.UserMember
{
   
    public class UserMemberService : IUserMemberService
    {
        readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;
        private readonly IUserMemberRepository _userMemberRepository;
        private readonly IEmailRepository _emailRepository;
        private readonly IUserRepository _userRepository;


        public UserMemberService(ILoggerManager loggerManager, IMapper mapper, IUserMemberRepository userMemberRepository, IEmailRepository emailRepository, IUserRepository userRepository)
        {
            _loggerManager = loggerManager;
            _mapper = mapper;
            _userMemberRepository = userMemberRepository;
            _emailRepository = emailRepository;
            _userRepository = userRepository;
        }


        public async Task<ResultWithDataDTO<int>> AddUserMember(AddUserMemberRequest_DTO request_DTO)
        {
            _loggerManager.LogInfo("Entry UserMemberService=> AddUserMember");
            ResultWithDataDTO<int> resultWithDataBO = new ResultWithDataDTO<int>
            {
                IsSuccessful = false
            };try 
            {
                var dataResult = await _userMemberRepository.AddUserMember(_mapper.Map<MasterProjectDAL.DataModel.UserMember>(request_DTO));
                if (dataResult != null)
                {
                    resultWithDataBO.Data = _mapper.Map<int>(1);
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"UserMember Save Successfully.";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }
                else
                {
                    resultWithDataBO.IsBusinessError = true;
                    resultWithDataBO.BusinessErrorMessage = $"Failed to Save UserMember Data'.\nKindly retry or contact System Administrator.";
                    _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                }

            }
            catch (Exception ex)
            {
                resultWithDataBO.IsBusinessError = true;
                resultWithDataBO.BusinessErrorMessage = $"Failed to add User Detail-Error observed during User Name: '{request_DTO.UserId}''.\nKindly retry or contact System Administrator.";
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


        public async Task<ResultWithDataDTO<List<GetUserMemberResponse_DTO>>> GetUserListMember()
        {

            _loggerManager.LogInfo("Entry UserMemberService=> GetUserMemberList");
            ResultWithDataDTO<List<GetUserMemberResponse_DTO>> resultWithDataBO = new ResultWithDataDTO<List<GetUserMemberResponse_DTO>>
            {
                IsSuccessful = false
            };
            try
            { 
                var dataResult = await _userMemberRepository.GetUserMembersList();
                if (dataResult != null) 
                    {
                        resultWithDataBO.Data = _mapper.Map<List<GetUserMemberResponse_DTO>>(dataResult);
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

        public async Task<ResultWithDataDTO<int>> UpdateUserMember(UpdateUserMemberRequest_DTO request_DTO)
        {
            _loggerManager.LogInfo("Entry UserMemberService=> UpdateUserMember");
            ResultWithDataDTO<int> resultWithDataBO = new ResultWithDataDTO<int>
            {
                IsSuccessful = false
            };
            try
            {
                var preExistData = await _userMemberRepository.GetUserMemberById(request_DTO.IduserMember);
                if (preExistData != null)
                {
                    var dataResult = await _userMemberRepository.UpdateUserMember(_mapper.Map<MasterProjectDAL.DataModel.UserMember>(request_DTO));
                    if (dataResult != null)
                    {
                        resultWithDataBO.Data = _mapper.Map<int>(1);
                        resultWithDataBO.IsSuccessful = true;
                        resultWithDataBO.Message = $"UserMember Updated Successfully.";
                        _loggerManager.LogInfo(resultWithDataBO.Message);
                    }
                    else
                    {
                        resultWithDataBO.IsBusinessError = true;
                        resultWithDataBO.BusinessErrorMessage = $"Failed to Save UserMember Data'.\nKindly retry or contact System Administrator.";
                        _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                    }
                }
                else
                {
                    resultWithDataBO.Data = _mapper.Map<int>(0);
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"UserMember not found.";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }

            }
            catch (Exception ex)
            {
                resultWithDataBO.IsBusinessError = true;
                resultWithDataBO.BusinessErrorMessage = $"Failed to update UserMember Detail-Error observed during UserMember Id: '{request_DTO.IduserMember}''.\nKindly retry or contact System Administrator.";
                resultWithDataBO.SystemErrorMessage = $"UserMemberService => UpdateUserMember: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message} and with request Object : {JsonSerializer.Serialize(request_DTO)}";
                _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                if (resultWithDataBO.SystemErrorMessage != null)
                {
                    _emailRepository.SendEmail(resultWithDataBO.SystemErrorMessage, $"UpdateUser");
                }
            }
            return resultWithDataBO;
        }

        public async Task<ResultWithDataDTO<int>> DeleteUserMember(UpdateUserMemberRequest_DTO request_DTO)
        {
            _loggerManager.LogInfo("Entry UserMemberService=> DeleteUserMember");
            ResultWithDataDTO<int> resultWithDataBO = new ResultWithDataDTO<int>
            {
                IsSuccessful = false
            };
            try
            {

                var preExistData = await _userMemberRepository.GetUserMemberById(request_DTO.IduserMember);
                if (preExistData != null)
                {
                    var dataResult = await _userMemberRepository.RemoveUserMember(_mapper.Map<MasterProjectDAL.DataModel.UserMember>(request_DTO));
                    if (dataResult != null)
                    {
                        resultWithDataBO.Data = _mapper.Map<int>(1);
                        resultWithDataBO.IsSuccessful = true;
                        resultWithDataBO.Message = $"UserMember Deleted Successfully.";
                        _loggerManager.LogInfo(resultWithDataBO.Message);
                    }
                    else
                    {
                        resultWithDataBO.IsBusinessError = true;
                        resultWithDataBO.BusinessErrorMessage = $"Failed to Save UserMember Data'.\nKindly retry or contact System Administrator.";
                        _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                    }
                }
                else
                {
                    resultWithDataBO.Data = _mapper.Map<int>(0);
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"UserMember not found.";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }

            }
            catch (Exception ex)
            {
                resultWithDataBO.IsBusinessError = true;
                resultWithDataBO.BusinessErrorMessage = $"Failed to add UserMember Detail-Error observed during UserMember Id: '{request_DTO.IduserMember}''.\nKindly retry or contact System Administrator.";
                resultWithDataBO.SystemErrorMessage = $"UserMemberService => DeleteUserMember: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message} and with request Object : {JsonSerializer.Serialize(request_DTO)}";
                _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                if (resultWithDataBO.SystemErrorMessage != null)
                {
                    _emailRepository.SendEmail(resultWithDataBO.SystemErrorMessage, $"DeleteUser");
                }
            }
            return resultWithDataBO;
        }


        public async Task<ResultWithDataDTO<GetUserMemberResponse_DTO>> GetUserByIdMember(int IdUser)
        {
            _loggerManager.LogInfo("Entry UserMemberService=> GetUserMemberById");
            ResultWithDataDTO<GetUserMemberResponse_DTO> resultWithDataBO = new ResultWithDataDTO<GetUserMemberResponse_DTO>
            {
                IsSuccessful = false
            };

            try
            {
                var dataResult = await _userMemberRepository.GetUserMemberById(IdUser);
                if (dataResult != null)
                {
                    resultWithDataBO.Data = _mapper.Map<GetUserMemberResponse_DTO>(dataResult);
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"UserMember Retrieved Successfully.";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }
                else
                {
                    resultWithDataBO.IsBusinessError = true;
                    resultWithDataBO.BusinessErrorMessage = $"Failed to Save UserMember Data'.\nKindly retry or contact System Administrator.";
                    _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                }

            }
            catch (Exception ex)
            {  
                resultWithDataBO.IsBusinessError = true;
                resultWithDataBO.BusinessErrorMessage = $"Failed to add UserMember Detail-Error observed during UserMember Id: '{IdUser}''.\nKindly retry or contact System Administrator.";
                resultWithDataBO.SystemErrorMessage = $"UserMemberService => GetUserMemberById: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message} and with request Id : {IdUser}";
                _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                if (resultWithDataBO.SystemErrorMessage != null)
                {
                    _emailRepository.SendEmail(resultWithDataBO.SystemErrorMessage, $"GetUserMemberById");
                }
            }
            return resultWithDataBO;
        }


        public async Task<ResultWithDataDTO<GetmemberChildListResponseList>> GetAllUsersByMemberName(string MemberName)
        {
            _loggerManager.LogInfo("Entry UserMemberService=> GetAllUsersByMemberName");
            ResultWithDataDTO<GetmemberChildListResponseList> resultWithDataBO = new ResultWithDataDTO<GetmemberChildListResponseList>
            {
                IsSuccessful = false
            };

            try
            {
                GetmemberChildListResponseList dataResult = new GetmemberChildListResponseList();
                //var use = new Userss();
                //var memberList = new List<GetUserMemberList>();
                var userData = await _userRepository.GetUserByName(MemberName);
                if(userData != null)
                {
                    dataResult.Usre = _mapper.Map<Userss>(userData);
                 
                    var memberData = await _userMemberRepository.GetAllUsersByMemberName(userData.IdUser);
                    if(memberData.Any() )
                    {
                        dataResult.GetUserMemberLists = _mapper.Map<List<GetUserMemberList>>(memberData);


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
                resultWithDataBO.BusinessErrorMessage = $"Failed to add UserMember Detail-Error observed during UserMember Id: '{MemberName}''.\nKindly retry or contact System Administrator.";
                resultWithDataBO.SystemErrorMessage = $"UserMemberService => GetAllUsersByMemberName: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message} and with request Id : {MemberName}";
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
