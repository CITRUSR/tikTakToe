using FluentValidation;

namespace server.Application.Services.Room.Dtos.Requests.CreateRoom;

public class CreateRoomRequestValidator : AbstractValidator<CreateRoomRequest>
{
    public CreateRoomRequestValidator()
    {
        RuleFor(x => x.OwnerId).NotEqual(Guid.Empty);
    }
}
