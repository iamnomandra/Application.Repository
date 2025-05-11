using Chat.Application.Entities;
using System.Linq.Expressions;

namespace Chat.Application.Services
{
    public interface IRepository<T> where T : class 
    {        
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<List<T>> GetListAllAsync();
        Task<T> GetByIdAsync(object _entityId);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null!);
        Task<(ICollection<T> Result, int TotalNumber, int TotalPages, bool IsPrevious, bool IsNext)> SearchOrderAndPaginateAsync(
                        Expression<Func<T, bool>> searchPredicate = null,
                        Expression<Func<T, object>> orderBy = null,
                        bool isDescending = false,
                        int? pageNumber = null,
                        int? pageSize = null,
                        params Expression<Func<T, object>>[] includeProperties);
                 
    }
}
