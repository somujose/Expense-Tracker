using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ExpenseTracker.Models
{
    public class ExpenseItemVM

    {
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Valid Decimal number with maximum 2 decimal places.")]
        [Display(Name ="Amount Spend")]
        public decimal Amount { get; set; }
        public int GroupId { get; set; }

        public string GroupName { get; set; }

        [Required(ErrorMessage = "Please Select User")]
        [Display(Name = "Amount Spend By")]
        public int UserId { get; set; }

        public List<SelectListItem> UserList { get; set; }
    }
}