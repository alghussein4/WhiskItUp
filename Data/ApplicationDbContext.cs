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

            // --- Seed Recipes ---
            builder.Entity<Recipe>().HasData(
                new Recipe { RecipeId = 1, RecipeName = "Pancakes", Description = "Fluffy breakfast pancakes", Time = 20, Servings = 4, Difficulty = EDifficulty.Easy, IsVegetarian = true, Calories = 350 },
                new Recipe { RecipeId = 2, RecipeName = "Chicken Alfredo", Description = "Creamy pasta with grilled chicken", Time = 45, Servings = 3, Difficulty = EDifficulty.Medium, IsVegetarian = false, Calories = 650 },
                new Recipe { RecipeId = 3, RecipeName = "Vegetable Soup", Description = "Healthy mixed vegetable soup", Time = 30, Servings = 5, Difficulty = EDifficulty.Easy, IsVegetarian = true, Calories = 200 },
                new Recipe { RecipeId = 4, RecipeName = "Chocolate Cake", Description = "Rich chocolate dessert", Time = 90, Servings = 8, Difficulty = EDifficulty.Hard, IsVegetarian = true, Calories = 450 }
            );

            // --- Seed Users ---
            builder.Entity<User>().HasData(
                new User { UserId = 1, FullName = "Alice Johnson Smith", Email = "alice@example.com", Gender = EGender.female, YearsOfExp = new DateTime(2015, 1, 1), PhoneNumber = "1234567890" },
                new User { UserId = 2, FullName = "Bob Alexander Brown", Email = "bob@example.com", Gender = EGender.male, YearsOfExp = new DateTime(2012, 5, 1), PhoneNumber = "0987654321" }
            );

            // --- Seed UserRecipeMapping ---
            builder.Entity<UserRecipeMapping>().HasData(
                new UserRecipeMapping { UserRecipeMappingId = 1, UserId = 1, RecipeId = 1, Comment = "Delicious and easy to make!", Rating = 5, Favorite = true },
                new UserRecipeMapping { UserRecipeMappingId = 2, UserId = 1, RecipeId = 3, Comment = "Healthy and tasty soup.", Rating = 4, Favorite = false },
                new UserRecipeMapping { UserRecipeMappingId = 3, UserId = 2, RecipeId = 2, Comment = "Creamy pasta, kids loved it.", Rating = 4.5, Favorite = true },
                new UserRecipeMapping { UserRecipeMappingId = 4, UserId = 2, RecipeId = 4, Comment = "Rich chocolate cake, perfect dessert.", Rating = 5, Favorite = false }
            );
        }
    }
}