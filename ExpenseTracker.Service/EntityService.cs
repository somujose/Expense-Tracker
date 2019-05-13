using ExpenseTracker.Model;
using ExpenseTracker.Repository;
using System;
using System.Collections.Generic;

namespace ExpenseTracker.Service
{
    public abstract class EntityService<T> : IEntityService<T> where T : BaseEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<T> _repository;

        public EntityService(IUnitOfWork unitOfWork, IGenericRepository<T> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public void Create(T entity)
        {
            if(entity == null)
                throw new ArgumentNullException("entity");

            _repository.Add(entity);
            _unitOfWork.Commit();
        }

        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            _repository.Delete(entity);
            _unitOfWork.Commit();
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            _repository.Edit(entity);
            _unitOfWork.Commit();
        }
    }
}
