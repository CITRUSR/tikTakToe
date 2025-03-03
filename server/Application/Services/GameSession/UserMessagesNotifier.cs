using System.Collections.Concurrent;
using server.Application.Services.GameSession.Dtos;

namespace server.Application.Services.GameSession;

public class UserMessagesNotifier : IUserMessagesNotifier
{
    private readonly ConcurrentDictionary<Guid, List<Action<SocketMessageEnvelope>>> _listeners =
    [];

    public void Subscribe(Guid userId, Action<SocketMessageEnvelope> action)
    {
        _listeners.AddOrUpdate(
            userId,
            key => new List<Action<SocketMessageEnvelope>> { action },
            (key, listeners) =>
            {
                listeners.Add(action);
                return listeners;
            }
        );
    }

    public void PublishMessage(SocketMessageEnvelope envelope)
    {
        if (_listeners.TryGetValue(envelope.UserConnection.User.Id, out var listeners))
        {
            foreach (var listener in listeners)
            {
                listener(envelope);
            }
        }
    }

    public void Unsubscribe(Guid userId)
    {
        _listeners.Remove(userId, out _);
    }

    public void UnsubscribeAll(Guid userId)
    {
        _listeners.TryRemove(userId, out _);
    }
}
