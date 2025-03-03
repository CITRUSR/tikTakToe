using FluentValidation;

namespace server.Application.Services.RoomManager.Dtos.Requests.ConnectToRoom;

public class ConnectToRoomRequestValidator : AbstractValidator<ConnectToRoomRequest>
{
    public ConnectToRoomRequestValidator()
    {
        RuleFor(x => x.RoomId).NotEqual(Guid.Empty);
        RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        RuleFor(x => x.Socket).NotNull();
    }
}
