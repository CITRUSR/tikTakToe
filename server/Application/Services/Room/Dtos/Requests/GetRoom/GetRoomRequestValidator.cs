using FluentValidation;

namespace server.Application.Services.Room.Dtos.Requests.GetRoom;

public class GetRoomRequestValidator : AbstractValidator<GetRoomRequest>
{
    public GetRoomRequestValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
    }
}
