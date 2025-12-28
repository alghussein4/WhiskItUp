using System.ComponentModel.DataAnnotations;

namespace WhiskItUp.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        //-----------------------------------
        public string? RecipeTitle { get; set; }
        //-----------------------------------
        public string? RecipeName { get; set; }
        //-----------------------------------
        public string? RecipeDescription { get; set; }
        //-----------------------------------
        [Range(5,120)]
        public double RecipeTime { get; set; }
        //-----------------------------------
        [Range(1,20)]
        public int Servings { get; set; }

        
    }
}
