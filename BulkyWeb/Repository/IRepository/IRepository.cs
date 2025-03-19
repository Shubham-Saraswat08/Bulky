using System.Linq.Expressions;

namespace BulkyBookWeb.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
        IEnumerable<T> GetAll(string? includeProperties = null);
        T? GetValue(Expression<Func<T, bool>> filter, string? includeProperties = null);
    }
}
