using Marajoara.Cinema.Management.Api.Exceptions;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace Marajoara.Cinema.Management.Api.Base
{
    public class ApiControllerBase : Controller
    {
        public IActionResult HandleResult<TFailure, TSuccess>(Result<TFailure, TSuccess> callback) where TFailure : Exception
        {
            return callback.IsSuccess ? Ok(callback.Success) : HandleFailure(callback.Failure);
        }

        public IActionResult HandleFailure<T>(T exceptionToHandle) where T : Exception
        {
            ExceptionPayload exceptionPayload = ExceptionPayload.New(exceptionToHandle);

            return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), exceptionPayload);
        }
    }
}
