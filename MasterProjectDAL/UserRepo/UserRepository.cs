using MasterProjectCommonUtility.Logger;
using MasterProjectDAL.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjectDAL.UserRepo
{
    public class UserRepository: IUserRepository
    {
        private IMasterProjectContext _masterProjectContext;
        private ILoggerManager _loggerManager;



        public UserRepository(IMasterProjectContext masterProjectContext, ILoggerManager loggerManager)
        {
            _masterProjectContext = masterProjectContext;
            _loggerManager = loggerManager;
        }
        public async Task<DataModel.User> AddUser(DataModel.User user)
        {
            _loggerManager.LogInfo("Entry UserRepository=> AddUser");
            await _masterProjectContext.User.AddAsync(user);
            await _masterProjectContext.SaveChangesAsync();
            _loggerManager.LogInfo("Exit UserRepository=> AddUser");
            return user;
        }

        public async Task<List<DataModel.User>> GetAllUserList()
        {
            _loggerManager.LogInfo("Entry UserRepository=> GetAllUserList");
            _loggerManager.LogInfo("Exit UserRepository=> GetAllUserList");
            return await _masterProjectContext.User.ToListAsync();
        }

        public async Task<DataModel.User?> GetUserById(int UserId, string keyword)
        {
            _loggerManager.LogInfo("Entry UserRepository=> GetUserById");
            _loggerManager.LogInfo("Exit UserRepository=> GetUserById");
            return await _masterProjectContext.User.Where(x => UserId ==0?true: x.IdUser == UserId && x.UserName.StartsWith(keyword)).FirstOrDefaultAsync();
        }

        public Task<User?> GetUserById(int UserId)
        {
            throw new NotImplementedException();
        }

        public async Task<DataModel.User> RemoveUser(DataModel.User user)
        {
            _loggerManager.LogInfo("Entry UserRepository=> RemoveUser");
            var dataResult = _masterProjectContext.User.Remove(user);
            await _masterProjectContext.SaveChangesAsync();
            _loggerManager.LogInfo("Exit UserRepository=> RemoveUser");
            return dataResult.Entity;
        }

        public async Task<DataModel.User> UpdateUser(DataModel.User user)
        {
            _loggerManager.LogInfo("Entry UserRepository=> UpdateUser");
            var dataResult = _masterProjectContext.User.Update(user);
            await _masterProjectContext.SaveChangesAsync();
            _loggerManager.LogInfo("Exit UserRepository=> UpdateUser");
            return dataResult.Entity;
        }

        public async Task<User?> GetUserByName (string userName)
        {
            return await _masterProjectContext.User.Where(t => t.UserName == userName).FirstOrDefaultAsync();
        }
    }
}

