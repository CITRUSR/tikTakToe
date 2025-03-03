using server.Application.Services.User.Dtos.Responses;

namespace server.Domain.Entities;

public class Game
{
    public Guid Id { get; set; }
    public List<UserViewModel> Players { get; set; }
    public UserViewModel? Winner { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}
