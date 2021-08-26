using System;

namespace TrueVUE.Cloud.SDK.API.Exceptions
{
    public class UnauthorizedServiceException : Exception
    {
        public UnauthorizedServiceException(string message) : base(message) { }
    }
}
