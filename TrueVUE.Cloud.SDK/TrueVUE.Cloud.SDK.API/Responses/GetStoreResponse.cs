using System;

namespace TrueVUE.Cloud.SDK.API.Responses
{
    public class GetStoreResponse
    {
        public Guid BusinessUnitId { get; set; }
        public Guid SiteId { get; set; }
        public string SiteName { get; set; }
        public string SiteCode { get; set; }
        public bool Active { get; set; }
    }
}
