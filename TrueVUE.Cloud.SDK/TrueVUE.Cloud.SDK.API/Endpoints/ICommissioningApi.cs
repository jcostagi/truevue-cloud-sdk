using Refit;
using System;
using System.Threading.Tasks;
using TrueVUE.Cloud.SDK.API.Requests;
using TrueVUE.Cloud.SDK.API.Responses;

namespace TrueVUE.Cloud.SDK.API.Endpoints
{
    public interface ICommissioningApi
    {
        [Post("/api/v1/commissioning/allocate")]
        Task<AllocateResponse> Allocate([Query] string apikey, [Query] Guid tenantId, [Body] AllocateRequest request);
    }
}
