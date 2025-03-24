namespace BulkyBookWeb.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository categoryRepository { get; }

        IProductRepository productRepository { get; }
        IAddToCart AddToCart { get; set; }

        void Save();
    }
}
