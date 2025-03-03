using FluentValidation;

namespace server.Application.Services.User.Dtos.Requests.GetUserById;

public class GetUserByIdRequestValidator : AbstractValidator<GetUserByIdRequest>
{
    public GetUserByIdRequestValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
    }
}
