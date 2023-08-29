using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyDemo.Core.Data.Entity;
using MyDemo.Core.Repositories.Interfaces;

namespace MyDemo.Core.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext _context { get; set; }
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll() => _context.Set<T>().AsNoTracking();

        public IQueryable<T> GetWithCondition(Expression<Func<T, bool>> expression) => 
            _context.Set<T>().Where(expression).AsNoTracking();

        public async Task<IEnumerable<T>> GetAllAsync() =>  await GetAll().ToListAsync();

        public async Task<IEnumerable<T>> GetWithConditionAsync(Expression<Func<T, bool>> expression) =>  
            await GetWithCondition(expression).ToListAsync();

        public async Task<T?> GetItemWithConditionAsync(Expression<Func<T, bool>> expression) =>  
            await GetWithCondition(expression).FirstOrDefaultAsync();

        public void Create(T entity) => _context.Set<T>().Add(entity);

        public void Update(T entity) => _context.Set<T>().Update(entity);

        public void Delete(T entity) => _context.Set<T>().Remove(entity);

        public int Save() => _context.SaveChanges();

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
    }
}