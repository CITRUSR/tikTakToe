using System.Text.RegularExpressions;
using FluentValidation;
using server.Application.Services.Auth.Dtos.Responses;

namespace server.Application.Services.Auth.Dtos.Requests.RegisterUser;

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.Nickname).MinimumLength(3).MaximumLength(20);
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters.");

        RuleFor(x => x.Password)
            .Must(HaveUppercaseLetter)
            .WithMessage("Password must have at least one uppercase letter.")
            .When(x => !string.IsNullOrEmpty(x.Password));

        RuleFor(x => x.Password)
            .Must(HaveLowercaseLetter)
            .WithMessage("Password must have at least one lowercase letter.")
            .When(x => !string.IsNullOrEmpty(x.Password));

        RuleFor(x => x.Password)
            .Must(HaveDigit)
            .WithMessage("Password must have at least one digit.")
            .When(x => !string.IsNullOrEmpty(x.Password));

        RuleFor(x => x.Password)
            .Must(HaveSpecialCharacter)
            .WithMessage("Password must have at least one special character.")
            .When(x => !string.IsNullOrEmpty(x.Password));

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
            .WithMessage("Confirm password should be equal password");
    }

    private bool HaveUppercaseLetter(string password) => Regex.IsMatch(password, ".*?[A-Z]");

    private bool HaveLowercaseLetter(string password) => Regex.IsMatch(password, ".*?[a-z]");

    private bool HaveDigit(string password) => Regex.IsMatch(password, ".*?[0-9]");

    private bool HaveSpecialCharacter(string password) =>
        Regex.IsMatch(password, ".*?[#?!@$%^&*-]");
}
