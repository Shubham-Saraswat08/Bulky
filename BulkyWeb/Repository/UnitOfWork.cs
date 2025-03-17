using Bulky.DataAccess.Data;
using BulkyWeb.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly BulkyWebDbContext dbContext;
        public CategoryRepository category { get; private set; }

        public UnitOfWork(BulkyWebDbContext dbContext)
        {
            this.dbContext = dbContext;
            category = new CategoryRepository(dbContext);
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
