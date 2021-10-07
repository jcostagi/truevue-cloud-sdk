using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrueVUE.Cloud.SDK.API.Responses;

namespace TrueVUE.Cloud.SDK.API.Endpoints
{
    public interface IProductApi
    {
        [Get("/api/v1/productCatalogs/getProductAttributesValues")]
        Task<List<GetProductInfoResponse>> GetProductInfo(
            [Query] string apikey, 
            [Query] Guid businessUnitId, 
            [Query] string productCodes, 
            [Query] string languageCode, 
            [Authorize("Bearer")] string token = null);

        [Get("/api/v1/productAttributes?size=500")]
        Task<ContentResponse<IEnumerable<ProductAttributesResponse>>> GetProductAttributes(
            [Query] string apikey, 
            [Query] Guid businessUnitId, 
            [Query] string productAttributeType, 
            [Authorize("Bearer")] string token = null);
    }
}
