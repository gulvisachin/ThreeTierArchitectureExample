using Microsoft.EntityFrameworkCore;
using PresentationLayer.DataAccessLayer;
using PresentationLayer.Infrastructure.Interfaces;
using System;

namespace PresentationLayer.Infrastructure.Repositorys
{
    public class CommonRepository<T> : ICommon<T> where T : class
    {
        private readonly TestingContext _context;
        private DbSet<T> _dbSet;

        public CommonRepository(TestingContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteAll(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>>? predicate = null, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if(includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }

            return query.AsNoTracking().ToList();
        }

        public T GetT(System.Linq.Expressions.Expression<Func<T, bool>>? predicate = null, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return query.AsNoTracking().FirstOrDefault();
        }
    }
}
