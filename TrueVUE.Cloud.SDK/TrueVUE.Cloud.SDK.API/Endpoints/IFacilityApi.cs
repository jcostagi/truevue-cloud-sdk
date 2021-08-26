using Refit;
using System.Threading.Tasks;
using TrueVUE.Cloud.SDK.API.Responses;

namespace TrueVUE.Cloud.SDK.API.Endpoints
{
    public interface IFacilityApi
    {
        [Get("/api/v1/sites")]
        Task<GetListResponse<GetStoreResponse>> GetSites([Query] string apikey, [Query] string businessUnitId, [Query] string type = "ALL");

        [Get("/api/v1/floors")]
        Task<GetListResponse<GetFloorResponse>> GetFloors([Query] string apikey, [Query] string siteId);
    }
}
