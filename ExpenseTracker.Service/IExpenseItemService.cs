using ExpenseTracker.Model.EF;
using System.Collections.Generic;

namespace ExpenseTracker.Service
{
    public interface IExpenseItemService : IEntityService<ExpenseItem>
    {
        IEnumerable<ExpenseItem> FindByIncludinguserDetails(int GroupId);
        IEnumerable<ExpenseItem> GetAllIncludingUserDetails();

        bool HasExpenseAdded(int GroupId);

    }
}
