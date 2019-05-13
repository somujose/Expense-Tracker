using ExpenseTracker.Model.EF;
using ExpenseTracker.Repository;

namespace ExpenseTracker.Service
{
    public class ExpenseGroupService : EntityService<ExpenseGroup>, IExpenseGroupService
    {
        private readonly IExpenseGroupRepository _repository;
        public ExpenseGroupService(IUnitOfWork unitOfWork, IExpenseGroupRepository repository)
            :base(unitOfWork, repository)
        {
            _repository = repository;
        }

        public ExpenseGroup GetById(int Id)
        {
            return _repository.GetById(Id);
        }
    }
}
