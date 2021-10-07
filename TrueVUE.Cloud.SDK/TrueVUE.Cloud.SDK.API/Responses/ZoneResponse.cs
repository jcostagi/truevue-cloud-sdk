using System;

namespace TrueVUE.Cloud.SDK.API.Responses
{
    public class ZoneResponse
    {
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public Guid FloorId { get; set; }
        public int OrderIndicator { get; set; }
        public Guid SiteId { get; set; }
        public Guid? SiteLayoutTemplateZoneId { get; set; }
        public Guid UserCreated { get; set; }
        public Guid UserUpdated { get; set; }
        public Guid ZoneId { get; set; }
        public string ZoneName { get; set; }
        public string ZoneType { get; set; }
    }
}
