namespace server.Domain.Exceptions.Room;

public class RoomNotFoundException : NotFoundException
{
    public RoomNotFoundException(Guid id)
        : base($"Room with id:{id} not found") { }
}
