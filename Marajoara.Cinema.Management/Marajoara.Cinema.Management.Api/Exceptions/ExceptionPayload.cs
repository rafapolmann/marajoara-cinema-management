using System;
using System.Text.Json.Serialization;

namespace Marajoara.Cinema.Management.Api.Exceptions
{
    public class ExceptionPayload
    {
        public string ErrorMessage { get; set; }

        [JsonIgnore]
        public Exception Exception { get; set; }

        public static ExceptionPayload New<T>(T exception) where T : Exception
        {
            return new ExceptionPayload
            {
                ErrorMessage = exception.Message,
                Exception = exception
            };
        }
    }
}
