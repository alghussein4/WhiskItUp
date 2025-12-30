using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace WhiskItUp.Models
{
    public class User
    {
        public int UserId { get; set; }
        //-----------------------------------
        [Required]
        [MaxLength(30)]
        [MinLength(15)]
        [Column(TypeName = "varchar(50)")]
        public required string FullName { get; set; }
        //-----------------------------------
        [EmailAddress]
        [Display(Prompt = "Enter Your Email")]
        public string? Email { get; set; }
        //-----------------------------------
        public EGender Gender { get; set; } = EGender.female;
        //-----------------------------------
        [Display(Name ="Years Of Experience")]
        [DataType(DataType.Date)]
        public DateTime YearsOfExp { get; set; }
        //-----------------------------------
        [Phone]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }




        //Navigation
        public IEnumerable<UserRecipeMapping>? UserRecipeMappings { get; set; }
    }
}
