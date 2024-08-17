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
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbContext.Set<T>().ToListAsync();

        /// <summary>
        /// Use FirstOrDefault when you expect a single result.
        /// Use Where to filter results based on multiple criteria and return a list.
        /// Use Contains for partial or case-insensitive matches.
        /// The Find method is best for primary key lookups.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }
        
        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate, string[] includes)
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

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }
        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, string[] includes)
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

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, string[] includes, int skipe, int take)
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

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, int? take, int? skip,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (predicate != null)
                query = _dbContext.Set<T>().Where(predicate);

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
        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, string[] includes, int? take, int? skip,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (predicate != null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if (take.HasValue)
                query = query.Take(take.Value);

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return await query.ToListAsync();
        }
        public async Task<T> Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }
        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public async Task<T> Update(T entity)
        {
            _dbContext.Update(entity);
            return entity;
        }

        public async void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async void DeleteRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
        }

        public async void Attach(T entity)
        {
            _dbContext.Set<T>().Attach(entity);
        }

        public async void AttachRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().AttachRange(entities);
        }

        public async Task<int> CountAsync()
        {
            return await _dbContext.Set<T>().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> criteria)
        {
            return await _dbContext.Set<T>().CountAsync(criteria);
        }
    }
}
