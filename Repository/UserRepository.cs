using System.Collections.Generic;
using System.Threading.Tasks;
using AuthApp.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using static AuthApp.Interface.IRepository;
using Microsoft.Extensions.Options;
namespace AuthApp.Repository
{
    public class UserRepository : IUser
    {
        private readonly dbContext _db;
        public UserRepository()
        {
            _db = new dbContext();
        }
        public async Task Add(UserInfo user)
        {
           await _db.Users.InsertOneAsync(user);
        }

        public async Task<IEnumerable<UserInfo>> Get()
        {
            return await _db.Users.Find(x=> true).ToListAsync();
        }

        public async Task<UserInfo> Get(string id)
        {
            return await _db.Users.Find(x=> x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<DeleteResult> Remove(string id)
        {
            return await _db.Users.DeleteOneAsync(Builders<UserInfo>.Filter.Eq("Id", id));
        }

        public async Task<DeleteResult> RemoveAll()
        {
            return await _db.Users.DeleteManyAsync(new BsonDocument());
        }

        public async Task Update(string id, UserInfo user)
        {
            await _db.Users.ReplaceOneAsync(m => m.Id == id, user);
        }
    }
}