namespace server.Domain.Exceptions.RoomConnection;

public class RoomConnectionNotFoundException : NotFoundException
{
    public RoomConnectionNotFoundException(Guid userId)
        : base($"Connection for user with id:{userId} not found") { }
}
