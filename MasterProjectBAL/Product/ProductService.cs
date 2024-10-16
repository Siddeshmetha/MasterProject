using AutoMapper;
using MasterProjectCommonUtility.Logger;
using MasterProjectCommonUtility.Response;
using MasterProjectDTOModel.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MasterProjectDAL.DataModel;
using MasterProjectDAL.Product;
using Microsoft.EntityFrameworkCore.Query;
using MasterProjectDAL.EmailRepo;
using System.Text.Json;

namespace MasterProjectBAL.Product
{
    public class ProductService : IProductService
    {
        readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IEmailRepository _emailRepository;
        public ProductService(ILoggerManager loggerManager, IMapper mapper, IProductRepository productRepository,IEmailRepository emailRepository)
        {
            _loggerManager = loggerManager;
            _mapper = mapper;
            _productRepository = productRepository;
            _emailRepository = emailRepository;
        }


        public async Task<ResultWithDataDTO<int>> AddProduct(AddProductRequest_DTO request_DTO)
        {
            _loggerManager.LogInfo("Entry ProductService=> AddProduct");
            ResultWithDataDTO<int> resultWithDataBO = new ResultWithDataDTO<int>
            {
                IsSuccessful = false
            };
            try
            {
                var dataResult = await _productRepository.AddProduct(_mapper.Map<MasterProjectDAL.DataModel.Product>(request_DTO));
                if (dataResult != null)
                {
                    resultWithDataBO.Data = _mapper.Map<int>(1);
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"Product Save Successfully.";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }
                else
                {
                    resultWithDataBO.IsBusinessError = true;
                    resultWithDataBO.BusinessErrorMessage = $"Failed to Save Product Data'.\nKindly retry or contact System Administrator.";
                    _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                }

            }
            catch (Exception ex)
            {
                resultWithDataBO.IsBusinessError = true;
                resultWithDataBO.BusinessErrorMessage = $"Failed to add Product Detail-Error observed during Product Name: '{request_DTO.ProductName }''.\nKindly retry or contact System Administrator.";
                resultWithDataBO.SystemErrorMessage = $"ProductService => AddProduct: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message}and with request Object : {JsonSerializer.Serialize(request_DTO)}";
                _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                if (resultWithDataBO.SystemErrorMessage != null)
                {
                    _emailRepository.SendEmail(resultWithDataBO.SystemErrorMessage, $"AddProduct");
                }
            }
            return resultWithDataBO;
        }

        public async Task<ResultWithDataDTO<int>> DeleteProduct(UpdateProductRequest_DTO request_DTO)
        {
            _loggerManager.LogInfo("Entry ProductService=> DeleteProduct");
            ResultWithDataDTO<int> resultWithDataBO = new ResultWithDataDTO<int>
            {
                IsSuccessful = false
            };
            try
            {
                var preExistData = await _productRepository.GetProductById(request_DTO.ProductId);
                if (preExistData != null)
                {
                    var dataResult = await _productRepository.RemoveProduct(_mapper.Map<MasterProjectDAL.DataModel.Product>(request_DTO));
                    if (dataResult != null)
                    {
                        resultWithDataBO.Data = _mapper.Map<int>(1);
                        resultWithDataBO.IsSuccessful = true;
                        resultWithDataBO.Message = $"Product Deleted Successfully.";
                        _loggerManager.LogInfo(resultWithDataBO.Message);
                    }
                    else
                    {
                        resultWithDataBO.IsBusinessError = true;
                        resultWithDataBO.BusinessErrorMessage = $"Failed to Save Product Data'.\nKindly retry or contact System Administrator.";
                        _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                    }
                }
                else
                {
                    resultWithDataBO.Data = _mapper.Map<int>(0);
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"Product not found.";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }

            }
            catch (Exception ex)
            {
                resultWithDataBO.IsBusinessError = true;
                resultWithDataBO.BusinessErrorMessage = $"Failed to add Product Detail-Error observed during Product Id: '{request_DTO.ProductId}''.\nKindly retry or contact System Administrator.";
                resultWithDataBO.SystemErrorMessage = $"ProductService => DeleteProduct: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message} and with request Object : {JsonSerializer.Serialize(request_DTO)}";
                _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                if (resultWithDataBO.SystemErrorMessage != null)
                {
                    _emailRepository.SendEmail(resultWithDataBO.SystemErrorMessage, $"DeleteProduct");
                }
            }
            return resultWithDataBO;
        }

        public async Task<ResultWithDataDTO<GetProductResponse_DTO>> GetProductById(int ProductId)
        {
            _loggerManager.LogInfo("Entry ProductService=> GetProductById");
            ResultWithDataDTO<GetProductResponse_DTO> resultWithDataBO = new ResultWithDataDTO<GetProductResponse_DTO>
            {
                IsSuccessful = false
            };
            try
            {
                var dataResult = await _productRepository.GetProductById(ProductId);
                if (dataResult != null)
                {
                    resultWithDataBO.Data = _mapper.Map<GetProductResponse_DTO>(dataResult);
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"Product Retrieved Successfully.";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }
                else
                {
                    resultWithDataBO.IsBusinessError = true;
                    resultWithDataBO.BusinessErrorMessage = $"Failed to Save Product Data'.\nKindly retry or contact System Administrator.";
                    _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                }

            }
            catch (Exception ex)
            {
                resultWithDataBO.IsBusinessError = true;
                resultWithDataBO.BusinessErrorMessage = $"Failed to add Product Detail-Error observed during Product Id: '{ProductId}''.\nKindly retry or contact System Administrator.";
                resultWithDataBO.SystemErrorMessage = $"ProductService => GetProductById: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message} and with request Id : {ProductId}";
                _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                if (resultWithDataBO.SystemErrorMessage != null)
                {
                    _emailRepository.SendEmail(resultWithDataBO.SystemErrorMessage, $"GetProductById");
                }
            }
            return resultWithDataBO;
        }

        public async Task<ResultWithDataDTO<List<GetProductResponse_DTO>>> GetProductList()
        {
            _loggerManager.LogInfo("Entry ProductService=> GetProductList");
            ResultWithDataDTO<List<GetProductResponse_DTO>> resultWithDataBO = new ResultWithDataDTO<List<GetProductResponse_DTO>>
            {
                IsSuccessful = false
            };
            try
            {
                var dataResult = await _productRepository.GetAllProductList();
                if (dataResult != null)
                {
                    resultWithDataBO.Data = _mapper.Map<List<GetProductResponse_DTO>>(dataResult);
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"Products Retrieved Successfully.";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }
                else
                {
                    resultWithDataBO.IsBusinessError = true;
                    resultWithDataBO.BusinessErrorMessage = $"Failed to Save Product Data'.\nKindly retry or contact System Administrator.";
                    _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                }

            }
            catch (Exception ex)
            {
                resultWithDataBO.IsBusinessError = true;
                resultWithDataBO.BusinessErrorMessage = $"Failed to add Product Detail-Error observed.\nKindly retry or contact System Administrator.";
                resultWithDataBO.SystemErrorMessage = $"ProductService => GetProductList: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message}";
                _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                if (resultWithDataBO.SystemErrorMessage != null)
                {
                    _emailRepository.SendEmail(resultWithDataBO.SystemErrorMessage, $"GetProductList");
                }
            }
            return resultWithDataBO;
        }

        public async Task<ResultWithDataDTO<int>> UpdateProduct(UpdateProductRequest_DTO request_DTO)
        {
            _loggerManager.LogInfo("Entry ProductService=> UpdateProduct");
            ResultWithDataDTO<int> resultWithDataBO = new ResultWithDataDTO<int>
            {
                IsSuccessful = false
            };
            try
            {
                var preExistData = await _productRepository.GetProductById(request_DTO.ProductId);
                if (preExistData != null)
                {
                    var dataResult = await _productRepository.UpdateProduct(_mapper.Map<MasterProjectDAL.DataModel.Product>(request_DTO));
                    if (dataResult != null)
                    {
                        resultWithDataBO.Data = _mapper.Map<int>(1);
                        resultWithDataBO.IsSuccessful = true;
                        resultWithDataBO.Message = $"Product Updated Successfully.";
                        _loggerManager.LogInfo(resultWithDataBO.Message);
                    }
                    else
                    {
                        resultWithDataBO.IsBusinessError = true;
                        resultWithDataBO.BusinessErrorMessage = $"Failed to Save Product Data'.\nKindly retry or contact System Administrator.";
                        _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                    }
                }
                else 
                {
                    resultWithDataBO.Data = _mapper.Map<int>(0);
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"Product not found.";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }

            }
            catch (Exception ex)
            {
                resultWithDataBO.IsBusinessError = true;
                resultWithDataBO.BusinessErrorMessage = $"Failed to update Product Detail-Error observed during Product Id: '{request_DTO.ProductId}''.\nKindly retry or contact System Administrator.";
                resultWithDataBO.SystemErrorMessage = $"ProductService => UpdateProduct: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message} and with request Object : {JsonSerializer.Serialize(request_DTO)}";
                _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                if (resultWithDataBO.SystemErrorMessage != null)
                {
                    _emailRepository.SendEmail(resultWithDataBO.SystemErrorMessage, $"UpdateProduct");
                }
            }
            return resultWithDataBO;
        }
    }
}
