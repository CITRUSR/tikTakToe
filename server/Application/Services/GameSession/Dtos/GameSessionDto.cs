using server.Application.Services.Map.Dtos;
using server.Application.Services.User.Dtos.Responses;
using server.Domain.Entities;

namespace server.Application.Services.GameSession.Dtos;

public class GameSessionDto
{
    public Guid Id { get; set; }
    public List<UserConnection> PlayerConnections { get; set; }
    public UserConnection ActivePlayerConnection { get; set; }
    public TimeSpan StartTime { get; set; }
    public GameBoard Map { get; set; }
    public bool GameInProcess { get; set; }
    public UserViewModel? Winner { get; set; }
    public TimeSpan? EndTime { get; set; }
};
