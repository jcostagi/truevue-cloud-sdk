using System;
using System.Net;

namespace TrueVUE.Cloud.SDK.API.Interfaces
{
    public interface IApiOptions
    {
        string BusinessUnitUrl { get; }
        Guid BusinessUnitId { get; }
        public Guid TenantId { get; set; }
        public string ApiKey { get; set; }
        public string Token { get; set; }
        DecompressionMethods Compression { get; }
        public bool IsAnonimousRequest { get; set; }
    }
}
