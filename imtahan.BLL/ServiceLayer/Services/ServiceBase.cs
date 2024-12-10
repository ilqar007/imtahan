using imtahan.BLL.DomainModel.Entities;
using imtahan.BLL.DomainModel.IRepositories;
using imtahan.BLL.ServiceLayer.Services.Interfaces;
using System.Linq.Expressions;

namespace imtahan.BLL.ServiceLayer.Services
{
    public class ServiceBase<T> : IService<T> where T : EntityBase
    {
        protected IRepo<T> repo;

        public ServiceBase(IRepo<T> repo)
        {
            this.repo = repo;
        }

        public async Task<int> CountAsync() => await repo.CountAsync();

        public bool HasChanges => repo.HasChanges;

        public async Task<int> AddAsync(T entity, bool persist = true)
        {
            return await repo.AddAsync(entity, persist);
        }

        public async Task<int> AddRangeAsync(IEnumerable<T> entities, bool persist = true)
        {
            return await repo.AddRangeAsync(entities, persist);
        }

        public async Task<int> DeleteAsync(T entity, bool persist = true)
        {
            return await repo.DeleteAsync(entity, persist);
        }

        public async Task<int> DeleteAsync(int id, byte[] timeStamp, bool persist = true)
        {
            return await repo.DeleteAsync(id, timeStamp, persist);
        }

        public async Task<int> DeleteRangeAsync(IEnumerable<T> entities, bool persist = true)
        {
            return await repo.DeleteRangeAsync(entities, persist);
        }

        public async Task<T> FindAsync(long? id)
        {
            return await repo.FindAsync(id);
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await repo.GetAllAsync();
        }

        public async Task<T> GetFirstAsync()
        {
            return await repo.GetFirstAsync();
        }

        public async Task<T> GetLastAsync()
        {
            return await repo.GetLastAsync();
        }

        public IEnumerable<T> GetRange(int skip, int take)
        {
            return repo.GetRange(skip, take);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await repo.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(T entity, bool persist = true)
        {
            return await repo.UpdateAsync(entity, persist);
        }

        public async Task<int> UpdateRangeAsync(IEnumerable<T> entities, bool persist = true)
        {
            return await repo.UpdateRangeAsync(entities, persist);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> whereCondition)
        {
            return await repo.GetSingleAsync(whereCondition);
        }

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> whereCondition)
        {
            return await repo.GetAllAsync(whereCondition);
        }

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> whereCondition, int pageNumber, int pageSize, Expression<Func<T, object>> orderByExpression)
        {
            return await repo.GetAllAsync(whereCondition, pageNumber, pageSize, orderByExpression);
        }

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> whereCondition, Expression<Func<T, object>> orderByExpression)
        {
            return await repo.GetAllAsync(whereCondition, orderByExpression);
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> whereCondition)
        {
            return repo.Query(whereCondition);
        }

        public async Task<long> CountAsync(Expression<Func<T, bool>> whereCondition)
        {
            return await repo.CountAsync(whereCondition);
        }
    }
}