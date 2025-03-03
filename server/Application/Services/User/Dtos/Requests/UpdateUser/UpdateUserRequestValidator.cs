using FluentValidation;

namespace server.Application.Services.User.Dtos.Requests.UpdateUser;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
        RuleFor(x => x.Nickname).NotEmpty().MaximumLength(128);
    }
}
