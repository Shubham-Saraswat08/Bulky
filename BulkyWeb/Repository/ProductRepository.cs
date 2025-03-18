using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using BulkyBookWeb.Repository.IRepository;

namespace BulkyBookWeb.Repository
{
    public class ProductRepository: Repository<Product>, IProductRepository {

        private readonly BulkyWebDbContext dbContext;

        public ProductRepository(BulkyWebDbContext dbContext): base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Update(Product obj)
        {
            dbContext.Products.Update(obj);
        }
    }
}
