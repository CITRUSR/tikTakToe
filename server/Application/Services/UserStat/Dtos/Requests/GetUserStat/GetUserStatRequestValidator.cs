using FluentValidation;

namespace server.Application.Services.UserStat.Dtos.Requests.GetUserStat;

public class GetUserStatRequestValidator : AbstractValidator<GetUserStatRequest>
{
    public GetUserStatRequestValidator()
    {
        RuleFor(x => x.UserId).NotEqual(Guid.Empty);
    }
}
