using System;
using System.Net;
using TrueVUE.Cloud.SDK.API.Interfaces;

namespace TrueVUE.Cloud.SDK.API.Services
{
    public class ServiceOptions : IServiceOptions
    {
        public string BusinessUnitUrl { get; set; }
        public string BusinessUnitId { get; set; }
        public Guid TenantId { get; set; }
        public string ApiKey { get; set; }
        public DecompressionMethods Compression { get; set; }
    }
}
