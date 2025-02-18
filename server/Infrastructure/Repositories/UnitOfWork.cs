using System.Data.Common;
using server.Application.Contracts.Repositories;
using server.Infrastructure.Factories;

namespace server.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public IUserRepository UserRepository { get; }

    public IRefreshTokenRepository RefreshTokenRepository { get; }

    public IUserStatRepository UserStatRepository { get; }

    private readonly DbConnection _connection;
    private DbTransaction _transaction;
    private bool _isDisposed;

    public UnitOfWork(
        IConnectionFactory connectionFactory,
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IUserStatRepository userStatRepository
    )
    {
        UserRepository = userRepository;
        RefreshTokenRepository = refreshTokenRepository;
        UserStatRepository = userStatRepository;

        _connection = connectionFactory.CreateConnection();

        _connection.Open();
    }

    public void CommitTransaction()
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("No transaction in progress");
        }

        try
        {
            _transaction.Commit();
        }
        catch
        {
            _transaction.Rollback();
            throw;
        }
        finally
        {
            Dispose();
        }
    }

    public void RollbackTransaction()
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("No transaction in progress");
        }

        if (_transaction != null)
        {
            _transaction.Rollback();
        }
    }

    public void BeginTransaction()
    {
        if (_transaction != null)
        {
            throw new InvalidOperationException("Transaction is already in progress");
        }

        _transaction = _connection.BeginTransaction();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public virtual void Dispose(bool disposing)
    {
        if (_isDisposed || !disposing)
            return;

        if (_transaction != null)
        {
            _transaction.Dispose();
        }
        if (_connection != null)
        {
            _connection.Close();
            _connection.Dispose();
        }

        _isDisposed = true;
    }
}
