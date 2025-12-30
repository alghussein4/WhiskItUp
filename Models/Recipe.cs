using System.ComponentModel.DataAnnotations;

namespace WhiskItUp.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        //-----------------------------------
        [Display(Name = "Recipe Name")]
        [MaxLength(35)]
        public string? RecipeName { get; set; }
        //-----------------------------------
        public string? Description { get; set; }
        //-----------------------------------
        [Range(5,120)]
        public double Time { get; set; }
        //-----------------------------------
        [Range(1,20)]
        public int Servings { get; set; }
        //-----------------------------------
        public EDifficulty Difficulty { get; set; } = EDifficulty.Easy;
        //-----------------------------------
        public bool IsVegetarian { get; set; } = false;
        //-----------------------------------
        [Range(0, 5000)]
        public int Calories { get; set; }

        //Navigation
        public IEnumerable<UserRecipeMapping>? UserRecipeMappings { get; set; }
    }
}
