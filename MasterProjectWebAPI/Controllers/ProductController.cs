using MasterProjectBAL.Product;
using MasterProjectCommonUtility.Logger;
using MasterProjectCommonUtility.Response;
using MasterProjectDTOModel.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterProjectWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IProductService _productService;
        public ProductController(ILoggerManager loggerManager, IProductService productService)
        {
            _loggerManager = loggerManager;
            _productService = productService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductRequest_DTO request_DTO)
        {
            ResultWithDataDTO<int> resultWithDataDTO =
                new ResultWithDataDTO<int> { IsSuccessful = false };
            if (request_DTO == null)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,Product Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry ProductController=> AddProduct");
            resultWithDataDTO = await _productService.AddProduct(request_DTO);
            _loggerManager.LogInfo("Exit ProductController=> AddProduct");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequest_DTO request_DTO)
        {
            ResultWithDataDTO<int> resultWithDataDTO =
                new ResultWithDataDTO<int> { IsSuccessful = false };
            if (request_DTO == null)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,Product Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry ProductController=> UpdateProduct");
            resultWithDataDTO = await _productService.UpdateProduct(request_DTO);
            _loggerManager.LogInfo("Exit ProductController=> UpdateProduct");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> DeleteProduct([FromBody] UpdateProductRequest_DTO request_DTO)
        {
            ResultWithDataDTO<int> resultWithDataDTO =
                new ResultWithDataDTO<int> { IsSuccessful = false };
            if (request_DTO == null)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,Product Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry ProductController=> DeleteProduct");
            resultWithDataDTO = await _productService.DeleteProduct(request_DTO);
            _loggerManager.LogInfo("Exit ProductController=> DeleteProduct");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }

        [HttpGet]
        [Route("[action]/{ProductId}")]
        public async Task<IActionResult> GetProductById(int ProductId)
        {
            ResultWithDataDTO<GetProductResponse_DTO> resultWithDataDTO =
                new ResultWithDataDTO<GetProductResponse_DTO> { IsSuccessful = false };
            if (ProductId == 0)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,Product Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry ProductController=> GetProductById");
            resultWithDataDTO = await _productService.GetProductById(ProductId);
            _loggerManager.LogInfo("Exit ProductController=> GetProductById");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetProductList()
        {
            ResultWithDataDTO<List<GetProductResponse_DTO>> resultWithDataDTO =
                new ResultWithDataDTO<List<GetProductResponse_DTO>> { IsSuccessful = false };
            _loggerManager.LogInfo("Entry ProductController=> GetProductList");
            resultWithDataDTO = await _productService.GetProductList();
            _loggerManager.LogInfo("Exit ProductController=> GetProductList");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }
    }
}
