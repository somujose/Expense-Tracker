using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models
{
    public class ExpenseGroupVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Group Name is Required")]
        [Display(Name = "Group Name")]
        public string Name { get; set; }

        public bool HasExpenses { get; set; }
    }
}