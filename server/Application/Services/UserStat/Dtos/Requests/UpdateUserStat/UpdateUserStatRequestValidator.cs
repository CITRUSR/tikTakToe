using FluentValidation;

namespace server.Application.Services.UserStat.Dtos.Requests.UpdateUserStat;

public class UpdateUserStatRequestValidator : AbstractValidator<UpdateUserStatRequest>
{
    public UpdateUserStatRequestValidator()
    {
        RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        RuleFor(x => x.Wins).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Losses).GreaterThanOrEqualTo(0);
        RuleFor(x => x.GamesCount).GreaterThanOrEqualTo(x => x.Wins + x.Losses);
    }
}
