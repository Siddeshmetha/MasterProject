using MasterProjectDAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjectDAL.UserRepo
{
    public interface IUserRepository
    {
        Task<DataModel.User> AddUser(DataModel.User user);
        Task<DataModel.User> UpdateUser(DataModel.User user);
        Task<DataModel.User?> GetUserById(int UserId);
        Task<List<DataModel.User>> GetAllUserList();
        Task<DataModel.User> RemoveUser(DataModel.User user);
        Task<User?> GetUserByName(string userName);
    }
}
