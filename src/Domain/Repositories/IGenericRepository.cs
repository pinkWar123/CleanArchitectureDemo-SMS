using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IGenericRepository<T> where T:class
    {
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<T?> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<T?> Delete(T entity);
    }
}