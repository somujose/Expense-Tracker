using ExpenseTracker.Model.EF;

namespace ExpenseTracker.Repository
{
    public interface IExpenseGroupRepository : IGenericRepository<ExpenseGroup>
    {
        ExpenseGroup GetById(int Id);
    }
}
