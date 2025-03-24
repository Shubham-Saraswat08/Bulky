using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using BulkyBookWeb.Repository.IRepository;

namespace BulkyBookWeb.Repository
{
    public class AddToCart : Repository<AddToCartModel>, IAddToCart
    {
        private readonly BulkyWebDbContext dbContext;

        public AddToCart(BulkyWebDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool UpdateCheck(AddToCartModel obj)
        {
            var data = dbContext.AddToCart.ToList().FirstOrDefault(val => val.productID == obj.productID && val.UserID == obj.UserID);
            if(data != null)
            {
                return true;
            }
            return false;
        }

        public void Update(AddToCartModel obj)
        {
            dbContext.AddToCart.Update(obj);
        }
    }
}
