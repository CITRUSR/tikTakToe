using FluentValidation;

namespace server.Application.Services.Auth.Dtos.Requests.RefreshUserToken;

public class RefreshUserTokenRequestValidator : AbstractValidator<RefreshUserTokenRequest>
{
    public RefreshUserTokenRequestValidator()
    {
        RuleFor(x => x.AccessToken).NotEmpty();
        RuleFor(x => x.RefreshToken).NotEmpty();
    }
}
