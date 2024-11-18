using StoredProcedureProject.Domains;

namespace StoredProcedureProject.DB
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        Task CommitAsync();
        Task CommitTransactionAsync();
        Task BeginTransactionAsync();
        Task RollbackAsync();
        Task DisposeAsync();

    }
}
