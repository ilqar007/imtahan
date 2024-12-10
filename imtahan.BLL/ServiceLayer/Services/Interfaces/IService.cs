using imtahan.BLL.DomainModel.Entities;
using System.Linq.Expressions;

namespace imtahan.BLL.ServiceLayer.Services.Interfaces
{
    public interface IService<T> where T : EntityBase
    {
        Task<T> GetSingleAsync(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition);

        Task<IList<T>> GetAllAsync(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition);

        Task<IList<T>> GetAllAsync(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition, int pageNumber, int pageSize, Expression<Func<T, object>> orderByExpression);

        Task<IList<T>> GetAllAsync(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition, Expression<Func<T, object>> orderByExpression);

        IQueryable<T> Query(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition);

        Task<long> CountAsync(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition);

        Task<int> CountAsync();

        bool HasChanges { get; }

        Task<T> FindAsync(long? id);

        Task<T> GetFirstAsync();

        Task<T> GetLastAsync();

        Task<IList<T>> GetAllAsync();

        IEnumerable<T> GetRange(int skip, int take);

        Task<int> AddAsync(T entity, bool persist = true);

        Task<int> AddRangeAsync(IEnumerable<T> entities, bool persist = true);

        Task<int> UpdateAsync(T entity, bool persist = true);

        Task<int> UpdateRangeAsync(IEnumerable<T> entities, bool persist = true);

        Task<int> DeleteAsync(T entity, bool persist = true);

        Task<int> DeleteRangeAsync(IEnumerable<T> entities, bool persist = true);

        Task<int> DeleteAsync(int id, byte[] timeStamp, bool persist = true);

        Task<int> SaveChangesAsync();
    }
}