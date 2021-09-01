using System;
using System.Net;
using TrueVUE.Cloud.SDK.API.Interfaces;

namespace TrueVUE.Cloud.SDK.API.Services
{
    public class ApiOptions : IApiOptions
    {
        public string BusinessUnitUrl { get; set; }
        public Guid BusinessUnitId { get; set; }
        public Guid TenantId { get; set; }
        public string ApiKey { get; set; }
        public string Token { get; set; }
        public DecompressionMethods Compression { get; set; }
    }
}
