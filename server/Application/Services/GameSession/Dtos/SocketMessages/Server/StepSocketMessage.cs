using server.Application.Services.Map.Dtos.Enums;

namespace server.Application.Services.GameSession.Dtos.SocketMessages.Server;

public class StepSocketMessage : SocketMessage
{
    public override string Type => "Step";
    public byte X { get; set; }
    public byte Y { get; set; }
    public CellState State { get; set; }
    public Domain.Entities.User User { get; set; }
}
