using Refit;
using System;
using System.Threading.Tasks;
using TrueVUE.Cloud.SDK.API.Requests;
using TrueVUE.Cloud.SDK.API.Responses;

namespace TrueVUE.Cloud.SDK.API.Endpoints
{
    public interface IPOSTransactionApi
    {
        [Post("/api/v1/pos/transaction/epc")]
        Task<POSTransactionEpcResponse> TransactEpc(
            [Query] Guid businessUnitId, 
            [Query] string apikey, 
            [Body] POSTransactionEpcRequest request);
    }
}
