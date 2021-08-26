using System;
using System.Net;

namespace TrueVUE.Cloud.SDK.API.Interfaces
{
    public interface IServiceOptions
    {
        string BusinessUnitUrl { get; }
        string BusinessUnitId { get; }
        public Guid TenantId { get; set; }
        public string ApiKey { get; set; }
        DecompressionMethods Compression { get; }
    }
}
