
namespace ExpenseTracker.Model.BL
{
    public sealed class ExpenseLineItem
    {
        public int PersonId { get; private set; }

        public string PersonName { get; private set; }
        public decimal AmountPaid { get; private set; }

        public ExpenseLineItem(int personId, string personName, decimal amount)
        {
            PersonId = personId;
            PersonName = personName;
            AmountPaid = amount;
        }
    }
}
