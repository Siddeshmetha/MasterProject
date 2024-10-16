using AutoMapper;
using MasterProjectCommonUtility.Logger;
using MasterProjectCommonUtility.Response;
using MasterProjectDAL.EmailRepo;
using MasterProjectDAL.RelationRepo;
using MasterProjectDTOModel.Relation;
using System.Text.Json;


namespace MasterProjectBAL.Relation
{
    public class RelationService : IRelationService
    {
        readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;
        private readonly IEmailRepository _emailRepository;
        private readonly IRelationRepo _relationRepo;
        
        public RelationService(ILoggerManager loggerManager, IMapper mapper, IRelationRepo relationRepo, IEmailRepository emailRepository)
        {
            _loggerManager = loggerManager;
            _mapper = mapper;
            _relationRepo = relationRepo;
            _emailRepository = emailRepository;

        }

        public async Task<ResultWithDataDTO<int>> AddRelation(AddRelationRequest_DTO request_DTO)
        {
            _loggerManager.LogInfo("Entry RelationService => RelationMember");
            ResultWithDataDTO<int> resultWithDataBO = new ResultWithDataDTO<int>
            {
                IsSuccessful = false
            }; try
            {
                var dataResult = await _relationRepo.AddRelation(_mapper.Map<MasterProjectDAL.DataModel.Relation>(request_DTO));
                if (dataResult != null)
                {
                    resultWithDataBO.Data = _mapper.Map<int>(1);
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"Relation Save Successfully.";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }
                else
                {
                    resultWithDataBO.IsBusinessError = true;
                    resultWithDataBO.BusinessErrorMessage = $"Failed to Save Relation'.\nKindly retry or contact System Administrator.";
                    _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                }

            }
            catch (Exception ex)
            {
                resultWithDataBO.IsBusinessError = true;
                resultWithDataBO.BusinessErrorMessage = $"Failed to add Relation Detail-Error observed during User Name: '{request_DTO.Idrelations}''.\nKindly retry or contact System Administrator.";
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

        public async Task<ResultWithDataDTO<List<GetRelationResponse_DTO>>> GetAllRelation()
        {
            _loggerManager.LogInfo("Entry RelationService => GetAllRelation");
            ResultWithDataDTO<List<GetRelationResponse_DTO>> resultWithDataBO = new ResultWithDataDTO<List<GetRelationResponse_DTO>>
            { IsSuccessful = false };
            try
            {
                var dataResult = await _relationRepo.GetAllRelations();
                if (dataResult != null)
                {
                    resultWithDataBO.Data = _mapper.Map<List<GetRelationResponse_DTO>>(dataResult);
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"Relation Retrieved Successfully.";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }
                else
                {
                    resultWithDataBO.IsBusinessError = true;
                    resultWithDataBO.BusinessErrorMessage = $"Failed to Save Relation Data'.\nKindly retry or contact System Administrator.";
                    _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                }
            }
            catch (Exception ex)
            {
                resultWithDataBO.IsBusinessError = true;
                resultWithDataBO.BusinessErrorMessage = $"Failed to add Relation Detail-Error observed.\nKindly retry or contact System Administrator.";
                resultWithDataBO.SystemErrorMessage = $"RelationService => GetRelationList: Exception Message: {ex.Message}\n" +
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
