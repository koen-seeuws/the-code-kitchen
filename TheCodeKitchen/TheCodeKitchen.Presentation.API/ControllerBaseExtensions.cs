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
            Fail: exception => Fail(controllerBase, exception)
        );

    public static ActionResult MatchActionResult<T>(
        this ControllerBase controllerBase,
        Result<T> result,
        Func<ControllerBase, T, ActionResult> onSuccess
    ) where T : notnull
        => result.Match(
            Succ: value => onSuccess(controllerBase, value),
            Fail: exception => Fail(controllerBase, exception)
        );

    public static ActionResult MatchActionResult(
        this ControllerBase controllerBase,
        Result<TheCodeKitchenUnit> result
    )
        => result.Match(
            Succ: _ => controllerBase.NoContent(),
            Fail: exception => Fail(controllerBase, exception)
        );

    private static ActionResult Fail(ControllerBase controllerBase, Exception exception)
    {
        switch (exception)
        {
            case NotFoundException:
                return controllerBase.NotFound();
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
            default:
                throw exception;
        }
    }
}