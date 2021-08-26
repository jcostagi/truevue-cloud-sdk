using System;
using System.Collections.Generic;

namespace TrueVUE.Cloud.SDK.API.Responses
{
    public class ProductAttributesResponse
    {
        public Guid ProductAttributeId { get; set; }
        public IEnumerable<GetProductAttributeLocalizationResponse> ProductAttributeLocalizations { get; set; }
    }
}
