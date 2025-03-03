namespace server.Domain.Entities;

public class Room
{
    public Guid Id { get; set; }
    public User Owner { get; set; }
    public User ConnectedUser { get; set; }
}
