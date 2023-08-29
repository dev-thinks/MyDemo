using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDemo.Core.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetWithCondition(Expression<Func<T, bool>> expression);

        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetWithConditionAsync(Expression<Func<T, bool>> expression);
        Task<T?> GetItemWithConditionAsync(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        int Save();
        Task<int> SaveAsync();
    }
}