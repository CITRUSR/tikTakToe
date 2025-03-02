using server.Domain.Entities;

namespace server.Application.Contracts.Services;

public interface IGameSessionSocketHelper
{
    Task SendToAllClientsAsync(List<UserConnection> userConnections, string msg);
    Task SendToAllClientsAsync(
        List<UserConnection> userConnections,
        string msg,
        List<UserConnection> exceptClients
    );
    Task SendErrorToClientsAsync(List<UserConnection> userConnections, string errorMsg);
}
