namespace server.Application.Contracts.Repositories;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }

    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}
