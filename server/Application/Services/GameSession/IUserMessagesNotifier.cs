using server.Application.Services.GameSession.Dtos;

namespace server.Application.Services.GameSession;

public interface IUserMessagesNotifier
{
    void PublishMessage(SocketMessageEnvelope envelope);
    void Subscribe(Guid userId, Action<SocketMessageEnvelope> action);
    void Unsubscribe(Guid userId);
    void UnsubscribeAll(Guid userId);
}
