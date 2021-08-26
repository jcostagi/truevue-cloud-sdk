using System.Collections.Generic;

namespace TrueVUE.Cloud.SDK.API.Requests
{
    public class ApplyTagMovesRequest
    {
        public string SiteId { get; set; }
        public IList<TagMoveRequest> Items { get; set; }
    }
}
