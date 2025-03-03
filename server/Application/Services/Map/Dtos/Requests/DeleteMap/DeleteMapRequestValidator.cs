using FluentValidation;

namespace server.Application.Services.Map.Dtos.Requests.DeleteMap;

public class DeleteMapRequestValidator : AbstractValidator<DeleteMapRequest>
{
    public DeleteMapRequestValidator()
    {
        RuleFor(x => x.GameId).NotEqual(Guid.Empty);
    }
}
