namespace BulkyWeb.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public CategoryRepository category { get;}
        void Save();
    }
}
