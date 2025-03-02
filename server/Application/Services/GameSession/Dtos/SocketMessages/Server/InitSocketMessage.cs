using server.Application.Services.Map.Dtos;
using server.Application.Services.User.Dtos.Responses;

namespace server.Application.Services.GameSession.Dtos.SocketMessages.Server;

public class InitSocketMessage : SocketMessage
{
    public override string Type => "Init";
    public GameBoardDto Map { get; set; }
    public UserViewModel ActiveUser { get; set; }
}
