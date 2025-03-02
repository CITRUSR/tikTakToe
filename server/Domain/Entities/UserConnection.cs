using System.Net.WebSockets;
using System.Text.Json.Serialization;
using server.Application.Services.User.Dtos.Responses;

namespace server.Domain.Entities;

public class UserConnection
{
    public UserViewModel User { get; set; }

    [JsonIgnore]
    public WebSocket WebSocket { get; set; }
}
