using Marajoara.Cinema.Management.Api.Exceptions;
using Marajoara.Cinema.Management.Api.Helpers;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace Marajoara.Cinema.Management.Api.Base
{
    public class ApiControllerBase : Controller
    {
        private readonly IHttpContextAccessor _context;
        public ApiControllerBase() { }

        public ApiControllerBase(IHttpContextAccessor context)
        {
            _context = context;
        }

        public IActionResult HandleResult<TFailure, TSuccess>(Result<TFailure, TSuccess> callback,
                                                              HttpStatusCode statusCode = HttpStatusCode.InternalServerError) where TFailure : Exception
        {
            return callback.IsSuccess ? Ok(callback.Success) : HandleFailure(callback.Failure, statusCode);
        }

        public IActionResult HandleFailure<T>(T exceptionToHandle, HttpStatusCode statusCode) where T : Exception
        {
            ExceptionPayload exceptionPayload = ExceptionPayload.New(exceptionToHandle);

            return StatusCode(statusCode.GetHashCode(), exceptionPayload);
        }

        protected Result<Exception, bool> ValitadeUserPermission(int userAccountID)
        {
            Result<Exception, bool> result = Result.Run(() =>
            {
                if (_context != null &&
                    (ClaimsHelper.GetUserAccountID(_context) == userAccountID ||
                     ClaimsHelper.GetRole(_context) == Domain.UserAccountModule.AccessLevel.Manager))
                    return true;
                else
                    throw new Exception("User does not have permission to access those datas.");
            });
            return result;
        }
    }
}
