using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Football.Api.Models.Errors;

public class BadRequestInvalidParamsDetails : ProblemDetails
{
    public BadRequestInvalidParamsDetails(IEnumerable<ValidationFailure> validationFailures) : base()
    {
        Title = "Validation Error";
        Detail = "Request parameter validation failed";
        Status = 400;
        Type = "https://footballscoreboard_api/validation-error";

        Extensions.Add("invalidParams", validationFailures.Select(e => new { e.PropertyName, e.ErrorMessage }));
    }
}
