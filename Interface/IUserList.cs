using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver;

using AuthApp.Models;

namespace AuthApp.Interface
{
    public class IRepository
    {
        public interface IUser
            {
                Task<IEnumerable<UserInfo>> Get();
                Task<UserInfo> Get(string id);
                Task Add(UserInfo user);
                Task Update(string id, UserInfo user);
                Task<DeleteResult> Remove(string id);
                Task<DeleteResult> RemoveAll();
            }
    }
}