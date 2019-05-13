using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ExpenseTracker.Models
{
    public class ExpenseUserVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Display name is Required")]
        [Display(Name = "Display Name")]
        [MaxLength(30,ErrorMessage ="Max Length for display name is 30")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Please provide a valid Email")]
        [Remote("IsAlreadyRegistered", "ExpenseTracker",HttpMethod ="POST",ErrorMessage ="Email Already Exists")]
        public string Email { get; set; }
    }
}