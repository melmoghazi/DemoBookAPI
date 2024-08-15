using DemoBookAPI.Core.Consts;
using DemoBookAPI.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DemoBookAPI.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected DemoBookAPIContext _dbContext { get; set; }
        public BaseRepository(DemoBookAPIContext dbContext)
        {
            _dbContext = dbContext;
        }
        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        //public async Task<IEnumerable<T>> GetAllAsync()
        //{
        //    return await _dbContext.Set<T>().ToListAsync();
        //}
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbContext.Set<T>().ToListAsync();

        public async Task<T> Find(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(predicate);
        }
        public async Task<T> Find(Expression<Func<T, bool>> predicate, string[] includes)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (predicate != null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            return await query.SingleOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }
        public async Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> predicate, string[] includes)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (predicate != null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            return await query.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> predicate, string[] includes, int skipe, int take)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (predicate != null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            return await query.Where(predicate).Skip(skipe).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? take, int? skip,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _dbContext.Set<T>().Where(criteria);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return await query.ToListAsync();
        }

        //adding
        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }
        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            _dbContext.SaveChanges();
            return entities;
        }
    }
}
