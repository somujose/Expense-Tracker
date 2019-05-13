using ExpenseTracker.Model.EF;
using System.Data.Entity;

namespace ExpenseTracker.Repository
{
    public class ExpenseUserRepository : GenericRepository<ExpenseUser>, IExpenseUserRepository
    {
        public ExpenseUserRepository(DbContext context)
            :base(context)
        {

        }
    }
}
