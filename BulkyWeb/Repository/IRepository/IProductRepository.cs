using BulkyBook.Models;

namespace BulkyBookWeb.Repository.IRepository
{
    public interface IProductRepository: IRepository<Product>
    {
        void Update(Product obj);
    }
}
