using System;
using System.Collections.Generic;

namespace TrueVUE.Cloud.SDK.API.Responses
{
    public class FloorResponse
    {
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public Guid FloorId { get; set; }
        public string FloorMapFilePath { get; set; }
        public string FloorName { get; set; }
        public int OrderIndicator { get; set; }
        public Guid SiteId { get; set; }
        public Guid? SiteLayoutTemplateFloorId { get; set; }
        public Guid UserCreated { get; set; }
        public Guid UserUpdated { get; set; }
        public List<ZoneResponse> Zones { get; set; }
    }
}
