
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Model
{
    public abstract class BaseEntity
    {
    }

    public abstract class Entity<T> : BaseEntity, IEntity<T>
    {
        [Key]
        public T Id { get; set; }
    }
}
