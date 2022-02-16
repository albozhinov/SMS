using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SMS.Data.Common
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SMSDbContext dbContext;

        public Repository(SMSDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public void Add(T entity)
        {
            EntityEntry entry = dbContext.Entry(entity);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                dbContext.Set<T>().Add(entity);
            }
        }

        public async Task AddAsync(T entity)
        {
            EntityEntry entry = dbContext.Entry(entity);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                await dbContext.Set<T>().AddAsync(entity);
            }
        }

        public IQueryable<T> All()
        {
            return dbContext.Set<T>().AsQueryable();
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            EntityEntry entry = dbContext.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                dbContext.Set<T>().Attach(entity);
            }
            
            entry.State = EntityState.Modified;
        }
    }
}
