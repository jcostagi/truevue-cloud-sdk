using Refit;
using System.Threading.Tasks;
using TrueVUE.Cloud.SDK.API.Responses;

namespace TrueVUE.Cloud.SDK.API.Endpoints
{
    public interface IBusinessUnitApi
    {
        [Get("/api/v1/businessUnits/taggingConfig/{businessUnitId}")]
        Task<GetTaggingConfigResponse> GetTaggingConfig(string businessUnitId, [Query] string apikey);
    }
}
