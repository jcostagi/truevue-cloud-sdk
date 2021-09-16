using System;

namespace TrueVUE.Cloud.SDK.API.Requests
{
    public class ReportTagWriteRequest
    {
        public string DeviceId { get; set; }
        public string ExpiryTime { get; set; }
        public string ProductId { get; set; }
        public long SerialNumber { get; set; }
        public Guid SiteId { get; set; }
        public string TagEncodingType { get; set; }
        public string TagWriteTime { get; set; }
        public Guid TenantId { get; set; }

    }
}
