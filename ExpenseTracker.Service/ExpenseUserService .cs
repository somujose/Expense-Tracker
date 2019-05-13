using ExpenseTracker.Model.EF;
using ExpenseTracker.Repository;

namespace ExpenseTracker.Service
{
    public class ExpenseUserService : EntityService<ExpenseUser>, IExpenseUserService
    {

        public ExpenseUserService(IUnitOfWork unitOfWork, IExpenseUserRepository repository)
            : base(unitOfWork, repository)
        {

        }
    }
}
