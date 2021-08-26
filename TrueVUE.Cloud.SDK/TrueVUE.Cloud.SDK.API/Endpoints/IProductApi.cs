using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrueVUE.Cloud.SDK.API.Responses;

namespace TrueVUE.Cloud.SDK.API.Endpoints
{
    public interface IProductApi
    {
        [Get("/api/v1/productCatalogs/getProductAttributesValues")]
        Task<List<GetProductInfoResponse>> GetProductInfo([Query] string apikey, [Query] string businessUnitId, [Query] string productCodes, [Query] string languageCode);

        [Get("/api/v1/productAttributes?size=500&productAttributeType=NONE")]
        Task<ContentResponse<IEnumerable<ProductAttributesResponse>>> GetProductAttributes([Query] string apikey, [Query] string businessUnitId);
    }
}
