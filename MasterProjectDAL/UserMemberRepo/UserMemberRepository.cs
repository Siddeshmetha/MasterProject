using MasterProjectCommonUtility.Logger;
using MasterProjectDTOModel;
using MasterProjectDAL.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterProjectDTOModel.UserMember;

namespace MasterProjectDAL.UserMemberRepo
{
    public class UserMemberRepository : IUserMemberRepository
    {
        private IMasterProjectContext _masterProjectContext;
        private ILoggerManager _loggerManager;

        public UserMemberRepository(IMasterProjectContext masterProjectContext, ILoggerManager loggerManager)
        {
            _masterProjectContext = masterProjectContext;
            _loggerManager = loggerManager;
        }

        public async Task<DataModel.UserMember> AddUserMember(UserMember userMember)
        {
            _loggerManager.LogInfo("Entry UserMemberRepository => AddUserMember");
            await _masterProjectContext.UserMember.AddAsync(userMember);
            await _masterProjectContext.SaveChangesAsync();
            _loggerManager.LogInfo("Exit UserMemberRepository=> AddUserMember");
            return userMember;
        }

        //public async Task<List<GetmemberChildListResponseList>> GetAllUsersByMemberName(string MemberName)
        //{
        //    _loggerManager.LogInfo("Entry UserMemberRepository => GetAllUsersByMemberName");
        //    var dataResult = await (
        //        from um in _masterProjectContext.UserMember
        //        join us in _masterProjectContext.User on um.UserId equals us.IdUser into UserMember
        //        from us in UserMember.DefaultIfEmpty()
        //        where um.MemberName == MemberName
        //        select new GetmemberChildListResponseList
        //        {
        //          UserName = us.UserName,
        //          Password = us.Password,
                 
               
        //        }).ToListAsync();
        //    return dataResult;
        //}

        public async Task<List<UserMember>> GetAllUsersByMemberName (int userId)
        {
           return await _masterProjectContext.UserMember.Where(q => q.UserId == userId).ToListAsync();

        }

        public async Task<UserMember> GetUserMemberById(int userMemberId)
        {
            _loggerManager.LogInfo("Entry UserMemberRepository => GetUserMemberById");
            var userMember = await _masterProjectContext.UserMember
                //.Include(um => um.User) 
                .FirstOrDefaultAsync(um => um.IduserMember == userMemberId);
            _loggerManager.LogInfo("Exit UserMemberRepository => GetUserMemberById");
            return userMember;
        }

        public async Task<List<UserMember>> GetUserMembersList()
        {
            //_loggerManager.LogInfo("Entry UserMemberRepository => GetUserMembersList");
            //var userMembers = await _masterProjectContext.UserMember
            //    .Include(um => um.User) 
            //    .ToListAsync();
            //_loggerManager.LogInfo("Exit UserMemberRepository => GetUserMembersList");
            //return userMembers;

            return await _masterProjectContext.UserMember.ToListAsync();
        }

        public async Task<UserMember> RemoveUserMember(UserMember userMember)
        {
            _loggerManager.LogInfo("Entry UserMemberRepository => RemoveUserMember");
            _masterProjectContext.UserMember.Remove(userMember);
            await _masterProjectContext.SaveChangesAsync();
            _loggerManager.LogInfo("Exit UserMemberRepository => RemoveUserMember");
            return userMember;
        }

        public async Task<UserMember> UpdateUserMember(UserMember userMember)
        {
            _loggerManager.LogInfo("Entry UserMemberRepository => UpdateUserMember");
            _masterProjectContext.UserMember.Update(userMember);
            await _masterProjectContext.SaveChangesAsync();
            _loggerManager.LogInfo("Exit UserMemberRepository => UpdateUserMember");
            return userMember;
        }

    }
}
