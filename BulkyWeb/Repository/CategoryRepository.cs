using Bulky.DataAccess.Data;
using Bulky.Models;
using BulkyWeb.Repository.IRepository;
using System.Linq.Expressions;

namespace BulkyWeb.Repository
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
