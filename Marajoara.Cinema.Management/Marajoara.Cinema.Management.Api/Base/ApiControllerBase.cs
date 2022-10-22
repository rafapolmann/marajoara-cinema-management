using Marajoara.Cinema.Management.Api.Exceptions;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace Marajoara.Cinema.Management.Api.Base
{
    public class ApiControllerBase : Controller
    {
        public IActionResult HandleResult<TFailure, TSuccess>(Result<TFailure, TSuccess> callback, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) where TFailure : Exception
        {
            return callback.IsSuccess ? Ok(callback.Success) : HandleFailure(callback.Failure, statusCode);
        }

        public IActionResult HandleFailure<T>(T exceptionToHandle, HttpStatusCode statusCode) where T : Exception
        {
            ExceptionPayload exceptionPayload = ExceptionPayload.New(exceptionToHandle);

            return StatusCode(statusCode.GetHashCode(), exceptionPayload);
        }
    }
}
