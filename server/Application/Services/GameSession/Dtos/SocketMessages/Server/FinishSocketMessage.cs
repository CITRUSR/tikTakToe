using server.Domain.Entities;

namespace server.Application.Services.GameSession.Dtos.SocketMessages.Server;

public class FinishSocketMessage : SocketMessage
{
    public override string Type => "Finish";
    public Game Game { get; set; }
}
