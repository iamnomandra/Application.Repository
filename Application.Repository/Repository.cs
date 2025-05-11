using System.Linq.Expressions;
using Chat.Application.Services; 
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Chat.Application.Entities.Data;

namespace Chat.Application.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbSet<T> mDbSet;
        private readonly ApplicationDbContext mDbContext; 

        public Repository(ApplicationDbContext context)
        {             
            mDbContext = context;
            mDbSet = mDbContext.Set<T>();
        }
        public async Task<T> AddAsync(T entity)
        { 
            try
            {
                await mDbContext.Database.BeginTransactionAsync();
                await mDbSet.AddAsync(entity);
                await mDbContext.SaveChangesAsync();
                await mDbContext.Database.CommitTransactionAsync();
            }
            catch(Exception e)
            {
                var error = e.InnerException == null ? e : e.InnerException;
                mDbContext.Database.RollbackTransaction(); 
                throw error;
            }
            finally
            {
                mDbContext.Database.BeginTransactionAsync().Dispose();
                this.mDbContext.Dispose();
            }

            return entity;
        }
        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            
            try
            {
                await mDbContext.Database.BeginTransactionAsync();
                await mDbSet.AddRangeAsync(entities);
                await mDbContext.SaveChangesAsync();
                await mDbContext.Database.CommitTransactionAsync();
            }
            catch (Exception e)
            {
                var error = e.InnerException == null ? e : e.InnerException;
                mDbContext.Database.RollbackTransaction();
                throw error;
            }
            finally
            {
                mDbContext.Database.BeginTransactionAsync().Dispose();
                this.mDbContext.Dispose();
            } 
        }
        public async Task<(ICollection<T> Result, int TotalNumber, int TotalPages, bool IsPrevious, bool IsNext)> SearchOrderAndPaginateAsync(Expression<Func<T, bool>> searchPredicate = null, Expression<Func<T, object>> orderBy = null, bool isDescending = false, int? pageNumber = null, int? pageSize = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = mDbSet;
            if (searchPredicate != null)
            {
                query = query.Where(searchPredicate);
            }
            if (orderBy != null)
            {
                if (isDescending)
                {
                    query = query.OrderByDescending(orderBy);
                }
                else
                {
                    query = query.OrderBy(orderBy);
                }
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            // Count total number of items without pagination
            int totalNumber = await query.CountAsync();

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            int? totalPages = pageNumber.HasValue && pageSize.HasValue
               ? (int?)Math.Ceiling((double)totalNumber / pageSize.Value)
               : null;
            bool? isPrevious = pageNumber.HasValue ? pageNumber > 1 : null;
            bool? isNext = pageNumber.HasValue && totalPages.HasValue ? pageNumber < totalPages : null;

            ICollection<T> result = await query.ToListAsync();

            return (result, totalNumber, totalPages ?? 0, isPrevious ?? false, isNext ?? false);
        }
        public async Task UpdateAsync(T entity)
        {
            try
            {
                await mDbContext.Database.BeginTransactionAsync();
                mDbSet.Attach(entity);
                mDbContext.Entry(entity).State = EntityState.Modified;
                await mDbContext.SaveChangesAsync();
                await mDbContext.Database.CommitTransactionAsync();
            }
            catch (Exception e)
            {
                var error = e.InnerException == null ? e : e.InnerException;
                mDbContext.Database.RollbackTransaction();
                throw error;
            }
            finally
            {
                mDbContext.Database.BeginTransactionAsync().Dispose();
                this.mDbContext.Dispose();
            }
        }
        public async Task DeleteAsync(T entity)
        {
            try
            {
                await mDbContext.Database.BeginTransactionAsync();
                mDbSet.Remove(entity);
                await mDbContext.SaveChangesAsync();
                await mDbContext.Database.CommitTransactionAsync();
            }
            catch (Exception e)
            {
                var error = e.InnerException == null ? e : e.InnerException;
                mDbContext.Database.RollbackTransaction();
                throw error;
            }
            finally
            {
                mDbContext.Database.BeginTransactionAsync().Dispose();
                this.mDbContext.Dispose();
            }
        }       
        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            int mCount = 0;
            try
            {
                IQueryable<T> query = mDbSet;
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }
                mCount = await query.CountAsync();
            }
            catch (Exception e)
            {
                var error = e.InnerException == null ? e : e.InnerException; 
                throw error;
            } 
            return mCount;
        }         
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var list = new List<T>();
            try
            {
                list =   await mDbSet.ToListAsync();
            }
            catch (Exception e)
            {
                var error = e.InnerException == null ? e : e.InnerException; 
                throw error;
            } 
            return list;
        }
        public async Task<List<T>> GetListAllAsync()
        {
            var list = new List<T>();
            try
            {
                list = await mDbSet.ToListAsync();
            }
            catch (Exception e)
            {
                var error = e.InnerException == null ? e : e.InnerException; 
                throw error;
            } 
            return list;
        }
        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            var list = new List<T>();
            try
            {
                list = await mDbSet.Where(match).ToListAsync();
            }
            catch (Exception e)
            {
                var error = e.InnerException == null ? e : e.InnerException; 
                throw error;
            } 
            return list;
        } 
        public async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            
            try
            {
                return await mDbSet.FirstOrDefaultAsync(match);
            }
            catch (Exception e)
            {
                var error = e.InnerException == null ? e : e.InnerException; 
                throw error;
            }  
        }
        public async Task<T> GetByIdAsync(object _entityId)
        {
            try
            {
                return await mDbSet.FindAsync(_entityId);
            }
            catch (Exception e)
            {
                var error = e.InnerException == null ? e : e.InnerException; 
                throw error;
            } 
        }
    }
}
