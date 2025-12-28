using System.ComponentModel.DataAnnotations;

namespace WhiskItUp.Models
{
    public class UserRecipeMapping
    {
        public int UserRecipeMappingId { get; set; }
        //-----------------------------------
        public int UserId { get; set; }
        public int RecipeId { get; set; }
        //-----------------------------------
        [StringLength(500,MinimumLength =10)]
        public String? Comment { get; set; }
        //-----------------------------------
        [Range(0,5)]
        public double Rating { get; set; }
        //-----------------------------------
        public bool Favorite { get; set; } = false;





        //Navegation
        public User? User { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
