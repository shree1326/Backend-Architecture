using Microservices.Customer.Data;
using Microservices.Customer.Entities;
using Microservices.Customer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Microservices.Customer.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly CustomerDBContext _context;
        private readonly DbSet<T> _entities;
        public GenericRepository(CustomerDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _entities = _context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _entities.SingleOrDefaultAsync(s => s.Id == id);
        }

        public ResponseModel Add(T entity)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                if (entity == null)
                {
                    responseModel.Message = "Can not add Order";
                    responseModel.Success = false;
                }
               else
                {
                    _entities.Add(entity);
                    responseModel.Message = "Order added successfully";
                    responseModel.Success = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return responseModel;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public async Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }
        //update method

        public ResponseModel Update(T entity)
        {
            //code to update entries using Id
            ResponseModel responseModel = new ResponseModel();
            try
            {
                if (entity == null)
                {
                    responseModel.Message = "Can not update Order";
                    responseModel.Success = false;
                }
                else
                {
                    _entities.Update(entity);
                    responseModel.Message = "Order updated successfully";
                    responseModel.Success = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return responseModel;
        }
        //delete method
        public ResponseModel Delete(T entity)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
               if (entity == null)
                {
                    responseModel.Message = "Can not delete Customer";
                    responseModel.Success = false;
                }
                else
                {
                    _entities.Remove(entity);
                    _context.SaveChanges();
                    responseModel.Message = "Customer deleted successfully";
                    responseModel.Success = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return responseModel;
        }

    }
}
