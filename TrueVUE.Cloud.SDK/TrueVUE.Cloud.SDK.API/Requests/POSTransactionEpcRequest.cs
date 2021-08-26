using System.Collections.Generic;

namespace TrueVUE.Cloud.SDK.API.Requests
{
    public class POSTransactionEpcRequest
    {
        public string SiteCode { get; set; }
        public List<POSTransactionEpcEventRequest> Transactions { get; set; }
    }
}
