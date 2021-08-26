using System;
using System.Net;

namespace TrueVUE.Cloud.SDK.API.Exceptions
{
    public class ApiConnectionException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        
        public ApiConnectionException(string message) : base(message) { }
        public ApiConnectionException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
