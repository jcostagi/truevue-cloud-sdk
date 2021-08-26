using System;
using System.Collections.Generic;

namespace TrueVUE.Cloud.SDK.API.Requests
{
    public class ApplyTagWritesRequest
    {
        public string DeviceId { get; set; }
        public string SiteId { get; set; }
        public IList<TagWriteRequest> Items { get; set; }
    }
}
