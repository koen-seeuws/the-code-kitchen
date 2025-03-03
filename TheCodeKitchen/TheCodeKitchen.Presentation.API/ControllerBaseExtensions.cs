

using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TheCodeKitchen.Core.Domain.Exceptions;
using TheCodeKitchen.Core.Shared;
using TheCodeKitchen.Core.Shared.Exceptions;
using TheCodeKitchen.Core.Shared.Monads;

namespace TheCodeKitchen.Presentation.API;

public static class ControllerBaseExtension
{
    /// <summary>
    /// Matches the result, on success resolves to a <see cref="OkResult"/> object that produces a <see cref="StatusCodes.Status200OK"/> response <br />
    /// In case of an exception, if its a <see cref="ValidationException"/> it will resolve to a <see cref="ValidationProblemDetails"/> response, if its a <see cref="NotFoundException"/> it will resolve to a <see cref="NotFoundResult"/> <br />
    /// else rethrow the exception.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="controllerBase">The controller base.</param>
    /// <param name="result">The result.</param>
    /// <returns></returns>
    public static ActionResult<T> MatchToAnActionResult<T>(this ControllerBase controllerBase, Result<T> result) where T : notnull
        => result
            .Match(arg => controllerBase.Ok(arg),
                failure => OnFailure(controllerBase, failure.Exception)
            );

    /// <summary>
    /// Matches the result, on success resolves the provided a <see param="onSuccess"/> object that produces a <see cref="ActionResult"/> response <br />
    /// In case of an exception, if its a <see cref="ValidationException"/> it will resolve to a <see cref="ValidationProblemDetails"/> response, if its a <see cref="NotFoundException"/> it will resolve to a <see cref="NotFoundResult"/> <br />
    /// else rethrow the exception.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="controllerBase">The controller base.</param>
    /// <param name="result">The result.</param>
    /// <param name="onSuccess">The func to execute on success result.</param>
    /// <returns></returns>
    public static ActionResult MatchToAnActionResult<T>(this ControllerBase controllerBase, Result<T> result, Func<ControllerBase, T, ActionResult> onSuccess) where T : notnull
        => result.Match(
            arg => onSuccess(controllerBase, arg), 
            failure => OnFailure(controllerBase, failure.Exception));



    /// <summary>
    /// Matches the result, on success resolves to a <see cref="NoContentResult"/> object that produces a <see cref="StatusCodes.Status204NoContent"/> response <br />
    /// In case of an exception, if its a <see cref="ValidationException"/> it will resolve to a <see cref="ValidationProblemDetails"/> response, if its a <see cref="NotFoundException"/> it will resolve to a <see cref="NotFoundResult"/> <br />
    /// else rethrow the exception.
    /// </summary>
    /// <param name="controllerBase">The controller base.</param>
    /// <param name="result">The result.</param>
    /// <returns></returns>
    public static ActionResult MatchToAnActionResult(this ControllerBase controllerBase, Result<TheCodeKitchenUnit> result)
        => result
            .Match(_ => controllerBase.NoContent(),
                failure => OnFailure(controllerBase, failure.Exception)
            );

    private static ActionResult OnFailure(ControllerBase controllerBase, Exception exception)
    {
        if (exception is NotFoundException)
        {
            return controllerBase.NotFound();
        }

        if (exception is TheCodeKitchenDomainException)
        {
            return controllerBase.BadRequest(new
            {
                IsDomainException = true,
                Code = exception.GetType().Name
            });
        }

        /*
        if (exception is ExternalHttpServiceException externalHttpServiceException)
        {
            return controllerBase.StatusCode((int) HttpStatusCode.ServiceUnavailable, new
            {
                IsExternalHttpServiceException = true,
                Code = externalHttpServiceException.Origin
            });
        }
        */

        if (exception is not ValidationException validationException)
        {
            throw exception;
        }

        var modelStateDictionary = new ModelStateDictionary();
        foreach (var error in validationException.Errors)
        {
            modelStateDictionary.AddModelError(error.PropertyName, error.ErrorMessage);
        }

        return controllerBase.ValidationProblem(modelStateDictionary);
    }
}