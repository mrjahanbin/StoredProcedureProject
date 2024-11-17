using StoredProcedureProject.Domains;

namespace StoredProcedureProject.DB
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> ProductRepository { get; }
        Task CommitAsync();
        Task CommitTransactionAsync();
        Task BeginTransactionAsync();
        Task RollbackAsync();
        Task DisposeAsync();

    }
}
