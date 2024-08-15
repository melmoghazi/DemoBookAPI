using DemoBookAPI.Core.Consts;
using System.Linq.Expressions;

namespace DemoBookAPI.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> Find(Expression<Func<T, bool>> match);
        Task<T> Find(Expression<Func<T, bool>> match, string[] includes);
        Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> predicate, string[] includes);
        Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> predicate, string[] includes, int skipe, int take);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? skip, int? take,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);
        T Add(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
    }
}
