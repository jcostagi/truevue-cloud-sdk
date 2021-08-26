using System;
using System.Collections.Generic;

namespace TrueVUE.Cloud.SDK.API.Responses
{
    public class AllocateResponse
    {
        public Guid TenantId { get; set; }
        public string ProductId { get; set; }
        public string DeviceId { get; set; }
        public DateTime ExpiryTime { get; set; }
        public string TagEncodingType { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public Guid UserCreated { get; set; }
        public Guid UserUpdated { get; set; }
        public IEnumerable<AllocatedRangeResponse> AllocatedRanges { get; set; }
    }
}
