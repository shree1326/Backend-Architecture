using Microservices.Customer.Entities;
using Microservices.Customer.Models;
using System.Linq.Expressions;

namespace Microservices.Customer.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        ResponseModel Add(T entity);
        Task<bool> SaveChangesAsync();
        Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate);
        //update method
        ResponseModel Update(T entity);
        //delete method
        ResponseModel Delete(T entity);
    }
}
