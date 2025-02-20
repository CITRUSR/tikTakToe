using FluentValidation;

namespace server.Application.Services.User.Dtos.Requests.GetUserByNickname;

public class GetUserByNicknameRequestValidator : AbstractValidator<GetUserByNicknameRequest>
{
    public GetUserByNicknameRequestValidator()
    {
        RuleFor(x => x.Nickname).NotNull().NotEmpty();
    }
}
