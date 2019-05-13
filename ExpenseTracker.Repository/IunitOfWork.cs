using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>Returns number of objects added,updated or Deleted</returns>
        int Commit();
    }
}
