using FluentValidation;

namespace server.Application.Services.Auth.Dtos.Requests.LoginUser;

public class LoginUserRequestValidator : AbstractValidator<LoginUserRequest>
{
    public LoginUserRequestValidator()
    {
        RuleFor(x => x.Nickname).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}
