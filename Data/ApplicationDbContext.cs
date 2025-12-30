using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WhiskItUp.Models;

namespace WhiskItUp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Recipe> tblRecipe { get; set; } = default!;
        public DbSet<User> tblUser { get; set; } = default!;
        public DbSet<UserRecipeMapping> tblUserRecipeMapping { get; set; } = default!;
    }
}
