using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using BulkyBookWeb.Repository.IRepository;
using System.Linq.Expressions;

namespace BulkyBookWeb.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly BulkyWebDbContext dbContext;

        public CategoryRepository(BulkyWebDbContext dbContext): base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Update(Category obj)
        {
            dbContext.Categories.Update(obj);
        }
    }
}
