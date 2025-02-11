namespace server.Application.Contracts.Repositories;

public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}
