using FluentValidation;

namespace server.Application.Services.Map.Dtos.Requests.GetMap;

public class GetMapRequestValidator : AbstractValidator<GetMapRequest>
{
    public GetMapRequestValidator()
    {
        RuleFor(x => x.GameId).NotEqual(Guid.Empty);
    }
}
