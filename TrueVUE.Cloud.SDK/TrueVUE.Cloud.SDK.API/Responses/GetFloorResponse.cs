using System;
using System.Collections.Generic;

namespace TrueVUE.Cloud.SDK.API.Responses
{
    public class GetFloorResponse
    {
        public Guid FloorId { get; set; }
        public string FloorName { get; set; }
        public IList<ZoneResponse> Zones { get; set; }
    }
}
