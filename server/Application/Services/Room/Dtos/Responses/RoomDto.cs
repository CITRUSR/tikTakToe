namespace server.Application.Services.Room.Dtos.Responses;

public record RoomDto(Guid Id, Domain.Entities.User Owner, Domain.Entities.User ConnectedUser);
