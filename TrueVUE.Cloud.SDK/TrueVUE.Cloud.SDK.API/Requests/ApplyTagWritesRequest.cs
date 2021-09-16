using System;
using System.Collections.Generic;

namespace TrueVUE.Cloud.SDK.API.Requests
{
    public class ApplyTagWritesRequest
    {
        public string DeviceId { get; set; }
        public Guid SiteId { get; set; }
        public IList<TagWriteRequest> Items { get; set; }
    }
}
