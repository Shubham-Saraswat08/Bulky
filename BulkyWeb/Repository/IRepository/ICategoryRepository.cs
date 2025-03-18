using BulkyBook.Models;

namespace BulkyBookWeb.Repository.IRepository
{
    public interface ICategoryRepository: IRepository<Category>
    {
        void Update(Category obj);
    }
}
