namespace server.Application.Services.GameSession.Dtos.SocketMessages.Server;

public class ErrorSocketMessage : SocketMessage
{
    public override string Type => "Error";
    public string Message { get; set; }
}
