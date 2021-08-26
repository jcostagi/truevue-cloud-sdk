using System;

namespace TrueVUE.Cloud.SDK.API.Exceptions
{
    public class NotConnectedException : Exception
    {
        public NotConnectedException(string message) : base(message) { }
    }
}
