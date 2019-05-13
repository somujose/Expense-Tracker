
using System.ComponentModel.DataAnnotations.Schema;


namespace ExpenseTracker.Model.EF
{
    [Table("ExpenseUser")]
    public class ExpenseUser : Entity<int>
    {
        
        public string DisplayName { get; set; }
        public string Email { get; set; }

    }
}
