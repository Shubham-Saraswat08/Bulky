using BulkyWeb.Repository.IRepository;
using Bulky.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace BulkyWeb.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly BulkyWebDbContext dbContext;
        internal DbSet<T> dbset;

        public Repository(BulkyWebDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbset = dbContext.Set<T>();
        }
        public void Add(T entity)
        {
            dbset.Add(entity);
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbset;
            return query.ToList();
        }

        public T? GetValue(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbset;
            return query.FirstOrDefault(filter);
        }

        public void Remove(T entity)
        {
            dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbset.RemoveRange(entity);
        }
    }
}
