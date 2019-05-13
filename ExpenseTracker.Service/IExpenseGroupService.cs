using ExpenseTracker.Model.EF;

namespace ExpenseTracker.Service
{
    public interface IExpenseGroupService : IEntityService<ExpenseGroup>
    {
        ExpenseGroup GetById(int Id);
    }
}
