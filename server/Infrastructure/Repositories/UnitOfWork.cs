using System.Data;
using server.Application.Contracts.Repositories;

namespace server.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    public IRefreshTokenRepository RefreshTokenRepository { get; }
    public IUserStatRepository UserStatRepository { get; }
    public IRoomRepository RoomRepository { get; }
    public IGameSessionRepository GameSessionRepository { get; }
    private readonly IDbConnection _connection;
    private IDbTransaction _transaction;
    private bool _isDisposed;

    public UnitOfWork(
        IUserRepository userRepository,
        IDbConnection connection,
        IRefreshTokenRepository refreshTokenRepository,
        IUserStatRepository userStatRepository,
        IRoomRepository roomRepository,
        IGameSessionRepository gameSessionRepository
    )
    {
        UserRepository = userRepository;
        RefreshTokenRepository = refreshTokenRepository;
        UserStatRepository = userStatRepository;
        RoomRepository = roomRepository;
        GameSessionRepository = gameSessionRepository;
        MapRepository = mapRepository;

        _connection = connection;

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
