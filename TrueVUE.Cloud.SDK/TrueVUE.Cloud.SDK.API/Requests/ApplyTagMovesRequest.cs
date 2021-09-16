using System;
using System.Collections.Generic;

namespace TrueVUE.Cloud.SDK.API.Requests
{
    public class ApplyTagMovesRequest
    {
        public Guid SiteId { get; set; }
        public IList<TagMoveRequest> Items { get; set; }
    }
}
