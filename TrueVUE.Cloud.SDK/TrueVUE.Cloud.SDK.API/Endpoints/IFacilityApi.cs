using Refit;
using System;
using System.Threading.Tasks;
using TrueVUE.Cloud.SDK.API.Responses;

namespace TrueVUE.Cloud.SDK.API.Endpoints
{
    public interface IFacilityApi
    {
        [Get("/api/v1/siteLayouts/{siteId}")]
        Task<GetSiteLayoutsResponse> GetSiteLayouts(
            [Query] string apikey, 
            Guid siteId, 
            [Authorize("Bearer")] string token = null);

        [Get("/api/v1/sites")]
        Task<GetListResponse<GetStoreResponse>> GetSites(
            [Query] string apikey, 
            [Query] Guid businessUnitId, 
            [Query] string type = "ALL");
    }
}
