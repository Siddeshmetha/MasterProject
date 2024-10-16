using AutoMapper;
using MasterProjectCommonUtility.Logger;
using MasterProjectCommonUtility.Response;
using MasterProjectDAL.EmailRepo;
using MasterProjectDAL.SchoolRepo;

using MasterProjectDTOModel.school;

using System.Text.Json;


namespace MasterProjectBAL.School
{
    public class SchoolService : ISchoolService
    {

        readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;
        private readonly IEmailRepository _emailRepository;
        private readonly ISchoolRepo _schoolRepo;

        public SchoolService(ILoggerManager loggerManager, IMapper mapper, ISchoolRepo schoolRepo, IEmailRepository emailRepository)
        {
            _loggerManager = loggerManager;
            _mapper = mapper;
            _schoolRepo = schoolRepo;
            _emailRepository = emailRepository;

        }


        public async Task<ResultWithDataDTO<int>> AddSchool(AddSchoolRequest_DTO request_DTO)
        {
            _loggerManager.LogInfo("Entry SchoolService=> SchoolMember");
            ResultWithDataDTO<int> resultWithDataBO = new ResultWithDataDTO<int>
            {
                IsSuccessful = false
            }; try
            {
                var dataResult = await _schoolRepo.AddSchool(_mapper.Map<MasterProjectDAL.DataModel.School>(request_DTO));
                if (dataResult != null)
                {
                    resultWithDataBO.Data = _mapper.Map<int>(1);
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"School Save Successfully.";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }
                else
                {
                    resultWithDataBO.IsBusinessError = true;
                    resultWithDataBO.BusinessErrorMessage = $"Failed to Save School'.\nKindly retry or contact System Administrator.";
                    _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                }

            }
            catch (Exception ex)
            {
                resultWithDataBO.IsBusinessError = true;
                resultWithDataBO.BusinessErrorMessage = $"Failed to add School Detail-Error observed during User Name: '{request_DTO.SuserId}''.\nKindly retry or contact System Administrator.";
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

        public async Task<ResultWithDataDTO<List<GetSchoolResponse_DTO>>> GetAllSchool()
        {
            _loggerManager.LogInfo("Entry SchoolService => GetAllSchool");
            ResultWithDataDTO<List<GetSchoolResponse_DTO>> resultWithDataBO = new ResultWithDataDTO<List<GetSchoolResponse_DTO>>
            { IsSuccessful = false };
            try
            {
                var dataResult = await _schoolRepo.GetAllSchool();
                if (dataResult != null)
                {
                    resultWithDataBO.Data = _mapper.Map<List<GetSchoolResponse_DTO>>(dataResult);
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"School Retrieved Successfully.";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }
                else
                {
                    resultWithDataBO.IsBusinessError = true;
                    resultWithDataBO.BusinessErrorMessage = $"Failed to Save School Data'.\nKindly retry or contact System Administrator.";
                    _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                }
            }
            catch (Exception ex)
            {
                resultWithDataBO.IsBusinessError = true;
                resultWithDataBO.BusinessErrorMessage = $"Failed to add School Detail-Error observed.\nKindly retry or contact System Administrator.";
                resultWithDataBO.SystemErrorMessage = $"SchoolService => GetSchoolList: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message}";
                _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                if (resultWithDataBO.SystemErrorMessage != null)
                {
                    _emailRepository.SendEmail(resultWithDataBO.SystemErrorMessage, $"GetUserList");
                }
            }
            return resultWithDataBO;

        }
    }
}
