using BulkyBook.Models;

namespace BulkyBookWeb.Repository.IRepository
{
    public interface IAddToCart : IRepository<AddToCartModel>
    {
        void Update(AddToCartModel obj);

        bool UpdateCheck(AddToCartModel obj);
    }
}
