using ExpenseTracker.Model.EF;
using ExpenseTracker.Repository;
using System.Collections.Generic;


namespace ExpenseTracker.Service
{
    public class ExpenseItemService : EntityService<ExpenseItem>, IExpenseItemService
    {
        private readonly IExpenseItemRepository _repository;
        public ExpenseItemService(IUnitOfWork unitOfWork, IExpenseItemRepository repository)
            :base(unitOfWork, repository)
        {
            _repository = repository;
        }

        public IEnumerable<ExpenseItem> FindByIncludinguserDetails(int GroupId)
        {
            return _repository.FindByIncludingUserDetails(GroupId);
        }

        public IEnumerable<ExpenseItem> GetAllIncludingUserDetails()
        {
            return _repository.GetAllIncludingUserDetails();
        }

        public bool HasExpenseAdded(int GroupId)
        {
            return _repository.HasExpenseAdded(GroupId);
        }
    }
}
