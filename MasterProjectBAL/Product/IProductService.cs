using MasterProjectCommonUtility.Response;
using MasterProjectDTOModel.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjectBAL.Product
{
    public interface IProductService
    {
        Task<ResultWithDataDTO<int>> AddProduct(AddProductRequest_DTO request_DTO);
        Task<ResultWithDataDTO<int>> UpdateProduct(UpdateProductRequest_DTO request_DTO);
        Task<ResultWithDataDTO<int>> DeleteProduct(UpdateProductRequest_DTO request_DTO);
        Task<ResultWithDataDTO<GetProductResponse_DTO>> GetProductById(int ProductId);
        Task<ResultWithDataDTO<List<GetProductResponse_DTO>>> GetProductList();
    }
}
