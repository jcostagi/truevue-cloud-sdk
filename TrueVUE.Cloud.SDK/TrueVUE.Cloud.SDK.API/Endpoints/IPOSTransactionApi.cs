using Refit;
using System.Threading.Tasks;
using TrueVUE.Cloud.SDK.API.Requests;
using TrueVUE.Cloud.SDK.API.Responses;

namespace TrueVUE.Cloud.SDK.API.Endpoints
{
    public interface IPOSTransactionApi
    {
        [Post("/api/v1/pos/transaction/epc/{businessUnitId}")]
        Task<POSTransactionEpcResponse> TransactEpc(string businessUnitId, [Query] string apikey, POSTransactionEpcRequest request);
    }
}
