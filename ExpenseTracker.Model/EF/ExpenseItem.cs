using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Model.EF
{
    [Table("ExpenseItem")]
    public class ExpenseItem : Entity<int>
    {
        public decimal Amount { get; set; }

        public int? ExpenseGroupId { get; set; }
        [ForeignKey("ExpenseGroupId")]
        public virtual ExpenseGroup Group { get; set; }


        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ExpenseUser User { get; set; }


    }
}
