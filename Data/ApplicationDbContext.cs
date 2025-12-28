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
        public DbSet<WhiskItUp.Models.Recipe> Recipe { get; set; } = default!;
        public DbSet<WhiskItUp.Models.User> User { get; set; } = default!;
        public DbSet<WhiskItUp.Models.UserRecipeMapping> UserRecipeMapping { get; set; } = default!;
    }
}
