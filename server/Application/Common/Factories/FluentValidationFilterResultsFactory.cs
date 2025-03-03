using FluentValidation.AspNetCore.Http;
using FluentValidation.Results;

namespace server.Application.Common.Factories;

public class FluentValidationFilterResultsFactory : IFluentValidationEndpointFilterResultsFactory
{
    public IResult Create(ValidationResult validationResult)
    {
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => x.ErrorMessage);

            return Results.BadRequest(errors);
        }

        return Results.Empty;
    }
}
