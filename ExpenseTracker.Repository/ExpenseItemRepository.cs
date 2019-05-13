using ExpenseTracker.Model.EF;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ExpenseTracker.Repository
{
    public class ExpenseItemRepository : GenericRepository<ExpenseItem>, IExpenseItemRepository
    {
        private DbContext _dbContext;
        public ExpenseItemRepository(DbContext dbContext)
            :base(dbContext)
        {

            _dbContext = dbContext;
        }

        public IEnumerable<ExpenseItem> FindByIncludingUserDetails(int GroupId)
        {
            return FindBy(x => x.ExpenseGroupId == GroupId).AsEnumerable();
        }

        public IEnumerable<ExpenseItem> GetAllIncludingUserDetails()
        {
            return _dbset.Include(x => x.User).AsEnumerable();
        }

        public ExpenseItem GetById(int id)
        {
            return FindBy(x => x.Id == id)
                 .FirstOrDefault();
        }

        public bool HasExpenseAdded(int GroupId)
        {
            return GetAll().ToList().Any(x => x.ExpenseGroupId == GroupId);
        }
    }
}
