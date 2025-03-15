using System.Net;
using FluentValidation;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TheCodeKitchen.Core.Domain.Abstractions;
using TheCodeKitchen.Core.Domain.Exceptions;
using TheCodeKitchen.Core.Shared;

namespace TheCodeKitchen.Presentation.API;

public static class ControllerBaseExtensions
{
    public static IActionResult MatchActionResult<T>(
        this ControllerBase controllerBase,
        Result<T> result
    ) where T : notnull
        => result.Match<IActionResult>(
            Succ: value => controllerBase.Ok(value),
            Fail: controllerBase.Fail
        );

    public static IActionResult MatchActionResult<T>(
        this ControllerBase controllerBase,
        Result<T> result,
        Func<ControllerBase, T, ActionResult> onSuccess
    ) where T : notnull
        => result.Match(
            Succ: value => onSuccess(controllerBase, value),
            Fail: controllerBase.Fail
        );

    public static IActionResult MatchActionResult(
        this ControllerBase controllerBase,
        Result<TheCodeKitchenUnit> result
    )
        => result.Match(
            Succ: _ => controllerBase.NoContent(),
            Fail: controllerBase.Fail
        );

    private static IActionResult Fail(this ControllerBase controllerBase, Exception exception)
    {
        switch (exception)
        {
            case NotFoundException:
                return controllerBase.NotFound(exception.Message);
            case DomainException:
                return controllerBase.BadRequest(new
                {
                    IsDomainException = true,
                    Code = exception.GetType().Name
                });
            case ValidationException validationException:
            {
                var modelStateDictionary = new ModelStateDictionary();
                foreach (var error in validationException.Errors)
                {
                    modelStateDictionary.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return controllerBase.ValidationProblem(modelStateDictionary);
            }
            case NotImplementedException:
                return controllerBase.StatusCode((int) HttpStatusCode.NotImplemented, "This operation has not been implemented yet.");
            default:
                return controllerBase.StatusCode((int) HttpStatusCode.InternalServerError, exception.Message);
        }
    }
}