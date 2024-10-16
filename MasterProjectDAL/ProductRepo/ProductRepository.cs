using MasterProjectCommonUtility.Logger;
using MasterProjectDAL.DataModel;
using MasterProjectDAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MasterProjectDAL.Product
{
    public class ProductRepository : IProductRepository
    {
        private IMasterProjectContext _masterProjectContext;
        private ILoggerManager _loggerManager;


        public ProductRepository(IMasterProjectContext masterProjectContext, ILoggerManager loggerManager) 
        {
            _masterProjectContext = masterProjectContext;
            _loggerManager= loggerManager;
        }
        public async Task<DataModel.Product> AddProduct(DataModel.Product product)
        {
            _loggerManager.LogInfo("Entry ProductRepository=> AddProduct");
            await _masterProjectContext.Product.AddAsync(product);
            await _masterProjectContext.SaveChangesAsync();
            _loggerManager.LogInfo("Exit ProductRepository=> AddProduct");
            return product;
        }

        public async Task<List<DataModel.Product>> GetAllProductList()
        {
            _loggerManager.LogInfo("Entry ProductRepository=> GetAllProductList");
            _loggerManager.LogInfo("Exit ProductRepository=> GetAllProductList");
            return await _masterProjectContext.Product.ToListAsync();
        }

        public async Task<DataModel.Product?> GetProductById(int ProductId)
        {
            _loggerManager.LogInfo("Entry ProductRepository=> GetProductById");
            _loggerManager.LogInfo("Exit ProductRepository=> GetProductById");
            return await _masterProjectContext.Product.Where(x => x.Id == ProductId).FirstOrDefaultAsync();
        }

        public async Task<DataModel.Product> RemoveProduct(DataModel.Product product)
        {
            _loggerManager.LogInfo("Entry ProductRepository=> RemoveProduct");
            var dataResult = _masterProjectContext.Product.Remove(product);
            await _masterProjectContext.SaveChangesAsync();
            _loggerManager.LogInfo("Exit ProductRepository=> RemoveProduct");
            return dataResult.Entity;
        }

        public async Task<DataModel.Product> UpdateProduct(DataModel.Product product)
        {
            _loggerManager.LogInfo("Entry ProductRepository=> UpdateProduct");
            var dataResult = _masterProjectContext.Product.Update(product);
            await _masterProjectContext.SaveChangesAsync();
            _loggerManager.LogInfo("Exit ProductRepository=> UpdateProduct");
            return dataResult.Entity;
        }
    }
}
