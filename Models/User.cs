using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;

namespace WhiskItUp.Models
{
    public class User
    {
        public int UserId { get; set; }
        //-----------------------------------
        [Required]
        [MaxLength(30)]
        [MinLength(15)]
        public string? FullName { get; set; }
        //-----------------------------------
        [EmailAddress]
        public string? Email { get; set; }
        //-----------------------------------
        public EGender Gender { get; set; } = EGender.female;
        //-----------------------------------


    }
}
