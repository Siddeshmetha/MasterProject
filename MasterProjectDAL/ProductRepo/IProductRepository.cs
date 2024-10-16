using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace MasterProjectDAL.Product
{
    public interface IProductRepository
    {
        Task<DataModel.Product> AddProduct(DataModel.Product product);
        Task<DataModel.Product> UpdateProduct(DataModel.Product product);
        Task<DataModel.Product?> GetProductById(int ProductId);
        Task<List<DataModel.Product>> GetAllProductList();
        Task<DataModel.Product> RemoveProduct(DataModel.Product product);
    }
}
