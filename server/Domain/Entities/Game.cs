namespace server.Domain.Entities;

public class Game
{
    public Guid Id { get; set; }
    public List<User> Players { get; set; }
    public User Winner { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}
