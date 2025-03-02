using FluentValidation;

namespace server.Application.Services.Map.Dtos.Requests.CreateMap;

public class CreateMapRequestValidator : AbstractValidator<CreateMapRequest>
{
    public CreateMapRequestValidator()
    {
        RuleFor(x => x.GameId).NotEqual(Guid.Empty);
    }
}
