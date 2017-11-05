using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Configuration;

namespace AuthApp.Models
{

    public class UserDbContext : IdentityDbContext<IdentityUser>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
                : base(options)
        {
        Database.EnsureCreated();
        }
    }

    public class dbContext
    {
        public IMongoDatabase _db { get; }
        public dbContext()
        {
            var connectionString = "mongodb://cybersecurity:&attack&@@1234@ds040877.mlab.com:40877/security";
            var db = "security";

            MongoClient client = new MongoClient(connectionString);
            _db = client.GetDatabase(db);
        }

        public IMongoCollection<UserInfo> Users
        {
            get
            {
                return _db.GetCollection<UserInfo>("UserList");
            }
        }
    }
}