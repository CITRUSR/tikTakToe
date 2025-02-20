namespace server.Domain.Entities;

public class UserStat
{
    public User User { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
    public int GamesCount { get; set; }
}
