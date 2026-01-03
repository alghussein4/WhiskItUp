using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WhiskItUp.Models.ModelView
{
    public class UserRecipesReport
    {
        //Search By...
        [Display(Name ="user id")]
        public int UserId { get; set; }
     
        [EmailAddress]
        [Display(Prompt = "Enter Your Email")]
        public string? Email { get; set; }
     
        public EGender Gender { get; set; } = EGender.female;

        //Results...
        public IEnumerable<User> users { get; set; } = new List<User>();
       

    }
}
