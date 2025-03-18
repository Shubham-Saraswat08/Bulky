using BulkyBook.DataAccess.Data;
using BulkyBookWeb.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BulkyWebDbContext dbContext;

        public ICategoryRepository categoryRepository { get; private set; }

        public IProductRepository productRepository { get; private set; }

        public UnitOfWork(BulkyWebDbContext dbContext)
        {
            this.dbContext = dbContext;
            categoryRepository = new CategoryRepository(dbContext);
            productRepository = new ProductRepository(dbContext);
        }



        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
