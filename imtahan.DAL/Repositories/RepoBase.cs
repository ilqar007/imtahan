using imtahan.BLL.DomainModel.Entities;
using imtahan.BLL.DomainModel.IRepositories;
using imtahan.DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace imtahan.DAL.Repositories
{
    public class RepoBase<T> : IRepo<T> where T : EntityBase, new()
    {
        protected readonly ImtahanContext Db;

        public RepoBase(ImtahanContext db)
        {
            this.Db = db;
            Table = Db.Set<T>();
        }

        protected DbSet<T> Table;

        public ImtahanContext Context => Db;

        public async Task<T> GetSingleAsync(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition)
        {
            return await Table.Where(whereCondition).FirstOrDefaultAsync();
        }

        public async Task<IList<T>> GetAllAsync(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition)
        {
            return await Table.Where(whereCondition).ToListAsync();
        }

        public async Task<IList<T>> GetAllAsync(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition, int pageNumber, int pageSize, System.Linq.Expressions.Expression<Func<T, object>> orderByExpression)
        {
            return await Table.Where(whereCondition).OrderByDescending(orderByExpression).Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IList<T>> GetAllAsync(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition, System.Linq.Expressions.Expression<Func<T, object>> orderByExpression)
        {
            return await Table.Where(whereCondition).OrderByDescending(orderByExpression).ToListAsync();
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await Table.ToListAsync();
        }

        public IQueryable<T> Query(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition)
        {
            return Table.Where(whereCondition);
        }

        public async Task<long> CountAsync(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition)
        {
            return await Table.Where(whereCondition).CountAsync();
        }

        public bool HasChanges => Db.ChangeTracker.HasChanges();

        public async Task<int> CountAsync() => await Table.CountAsync();

        public async Task<T> GetFirstAsync() => await Table.FirstOrDefaultAsync();

        public async Task<T> GetLastAsync() => await Table.LastOrDefaultAsync();

        public async Task<T> FindAsync(long? id) => await Table.FindAsync(id);

        internal IEnumerable<T> GetRange(IQueryable<T> query, int skip, int take)
           => query.Skip(skip).Take(take);

        public virtual IEnumerable<T> GetRange(int skip, int take)
            => GetRange(Table, skip, take);

        public virtual async Task<int> AddAsync(T entity, bool persist = true)
        {
            Table.Add(entity);
            return persist ? await SaveChangesAsync() : 0;
        }

        public virtual async Task<int> AddRangeAsync(IEnumerable<T> entities, bool persist = true)
        {
            Table.AddRange(entities);
            return persist ? await SaveChangesAsync() : 0;
        }

        public virtual async Task<int> UpdateAsync(T entity, bool persist = true)
        {
            Table.Update(entity);
            return persist ? await SaveChangesAsync() : 0;
        }

        public virtual async Task<int> UpdateRangeAsync(IEnumerable<T> entities, bool persist = true)
        {
            Table.UpdateRange(entities);
            return persist ? await SaveChangesAsync() : 0;
        }

        public virtual async Task<int> DeleteAsync(T entity, bool persist = true)
        {
            Table.Remove(entity);
            return persist ? await SaveChangesAsync() : 0;
        }

        public virtual async Task<int> DeleteRangeAsync(IEnumerable<T> entities, bool persist = true)
        {
            Table.RemoveRange(entities);
            return persist ? await SaveChangesAsync() : 0;
        }

        /*   internal T GetEntryFromChangeTracker(int? id)
           {
               return Db.ChangeTracker.Entries<T>()
                   .Select((EntityEntry e) => (T)e.Entity)
                       .FirstOrDefault(x => x.Id == id);
           }*/

        //TODO: Check For Cascade Delete
        public async Task<int> DeleteAsync(int id, byte[] timeStamp, bool persist = true)
        {
            //var entry = GetEntryFromChangeTracker(id);
            //if (entry != null)
            //{
            //    if (timeStamp != null && entry.TimeStamp.SequenceEqual(timeStamp))
            //    {
            //        return await DeleteAsync(entry, persist);
            //    }
            //    throw new Exception("Unable to delete due to concurrency violation.");
            //}
            //Db.Entry(new T { Id = id, TimeStamp = timeStamp }).State = EntityState.Deleted;
            //return persist ? await SaveChangesAsync() : 0;
            return 0;
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await Db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //A concurrency error occurred
                //Should handle intelligently
                Console.WriteLine(ex);
                throw;
            }
            catch (RetryLimitExceededException ex)
            {
                //DbResiliency retry limit exceeded
                //Should handle intelligently
                Console.WriteLine(ex);
                throw;
            }
            catch (Exception ex)
            {
                //Should handle intelligently
                Console.WriteLine(ex);
                throw;
                //-2146232060
                //throw new Exception($"{ex.HResult}");
            }
        }

        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }
            Db.Dispose();
            _disposed = true;
        }
    }
}