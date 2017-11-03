using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace AuthApp.Models
{
    public class GroceryListContext : DbContext
    {
        public GroceryListContext(DbContextOptions<GroceryListContext> options)
            : base(options)
        {
        }

        public DbSet<GroceryItem> GroceryList { get; set; }
    }

    public class UserDbContext : IdentityDbContext<IdentityUser>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
                : base(options)
        {
        Database.EnsureCreated();
        }
    }
}