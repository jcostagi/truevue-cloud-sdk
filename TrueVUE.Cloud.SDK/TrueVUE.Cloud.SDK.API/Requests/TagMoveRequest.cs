using System;

namespace TrueVUE.Cloud.SDK.API.Requests
{
    public class TagMoveRequest
    {
        public string Epc { get; set; }
        public string EventDate { get; set; }
        public Guid ZoneId { get; set; }
    }
}