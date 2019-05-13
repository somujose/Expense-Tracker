using ExpenseTracker.Model.EF;
using System.Collections.Generic;

namespace ExpenseTracker.Repository
{
    public interface IExpenseItemRepository : IGenericRepository<ExpenseItem>
    {
        IEnumerable<ExpenseItem> FindByIncludingUserDetails(int GroupId);
        IEnumerable<ExpenseItem> GetAllIncludingUserDetails();

        bool HasExpenseAdded(int GroupId);

        ExpenseItem GetById(int id);
    }
}
