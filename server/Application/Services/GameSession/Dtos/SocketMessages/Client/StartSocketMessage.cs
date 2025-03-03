namespace server.Application.Services.GameSession.Dtos.SocketMessages.Client;

public class StartSocketMessage : SocketMessage
{
    public override string Type => "Start";
}
