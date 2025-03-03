namespace server.Application.Contracts.Repositories;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    IRefreshTokenRepository RefreshTokenRepository { get; }
    IUserStatRepository UserStatRepository { get; }
    IRoomRepository RoomRepository { get; }
    IGameSessionRepository GameSessionRepository { get; }
    IMapRepository MapRepository { get; }

    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}
