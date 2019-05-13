
using System.ComponentModel.DataAnnotations.Schema;


namespace ExpenseTracker.Model.EF
{
    [Table("ExpenseGroup")]
    public class ExpenseGroup :Entity<int>
    {
        public string Name { get; set; }
    }
}
