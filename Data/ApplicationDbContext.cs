using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using WhiskItUp.Data.wip;
using WhiskItUp.Models;

namespace WhiskItUp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        // Tables
        public DbSet<Recipe> tblRecipe { get; set; } = default!;
        public DbSet<User> tblUser { get; set; } = default!;
        public DbSet<UserRecipeMapping> tblUserRecipeMapping { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // --- Seed Recipes --- (IDs جديدة وأسماء مختلفة)
            builder.Entity<Recipe>().HasData(
                new Recipe { RecipeId = 101, RecipeName = "Banana Pancakes", Description = "Fluffy banana breakfast pancakes", Time = 25, Servings = 2, Difficulty = EDifficulty.Easy, IsVegetarian = true, Calories = 300 },
                new Recipe { RecipeId = 102, RecipeName = "Spaghetti Carbonara", Description = "Creamy spaghetti with bacon", Time = 40, Servings = 4, Difficulty = EDifficulty.Medium, IsVegetarian = false, Calories = 600 },
                new Recipe { RecipeId = 103, RecipeName = "Tomato Soup", Description = "Fresh and healthy tomato soup", Time = 35, Servings = 3, Difficulty = EDifficulty.Easy, IsVegetarian = true, Calories = 180 },
                new Recipe { RecipeId = 104, RecipeName = "Lemon Cheesecake", Description = "Tangy lemon dessert", Time = 80, Servings = 6, Difficulty = EDifficulty.Hard, IsVegetarian = true, Calories = 500 }
            );

            // --- Seed Users --- (IDs وأسماء جديدة)
            builder.Entity<User>().HasData(
                new User { UserId = 101, FullName = "Clara Evans", Email = "clara@example.com", Gender = EGender.female, YearsOfExp = new DateTime(2016, 2, 15), PhoneNumber = "5551112222" },
                new User { UserId = 102, FullName = "David Miller", Email = "david@example.com", Gender = EGender.male, YearsOfExp = new DateTime(2013, 7, 10), PhoneNumber = "5553334444" }
            );

            // --- Seed UserRecipeMapping --- (IDs جديدة وتربط البيانات الجديدة)
            builder.Entity<UserRecipeMapping>().HasData(
                new UserRecipeMapping { UserRecipeMappingId = 101, UserId = 101, RecipeId = 101, Comment = "Perfect banana pancakes!", Rating = 5, Favorite = true },
                new UserRecipeMapping { UserRecipeMappingId = 102, UserId = 101, RecipeId = 103, Comment = "Tomato soup is very tasty.", Rating = 4, Favorite = false },
                new UserRecipeMapping { UserRecipeMappingId = 103, UserId = 102, RecipeId = 102, Comment = "Carbonara was amazing.", Rating = 4.5, Favorite = true },
                new UserRecipeMapping { UserRecipeMappingId = 104, UserId = 102, RecipeId = 104, Comment = "Lemon cheesecake is delicious.", Rating = 5, Favorite = false }
            );
        }

    }
}