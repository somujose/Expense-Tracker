using ExpenseTracker.Model.EF;
using System.Data.Entity;
using System.Linq;


namespace ExpenseTracker.Repository
{
    public class ExpenseGroupRepository : GenericRepository<ExpenseGroup>, IExpenseGroupRepository
    {
        public ExpenseGroupRepository(DbContext context)
            : base(context)
        {

        }
        public ExpenseGroup GetById(int Id)
        {
            return FindBy(x => x.Id == Id).FirstOrDefault();
        }
    }
}
