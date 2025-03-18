namespace BulkyBookWeb.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository categoryRepository { get; }

        IProductRepository productRepository { get; }
        void Save();
    }
}
