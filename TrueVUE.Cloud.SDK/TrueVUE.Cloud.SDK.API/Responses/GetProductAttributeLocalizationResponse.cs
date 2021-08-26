using System;

namespace TrueVUE.Cloud.SDK.API.Responses
{
    public class GetProductAttributeLocalizationResponse
    {
        public Guid ProductAttributeLocalizationId { get; set; }
        public string LanguageCode { get; set; }
        public string DisplayName { get; set; }
        public string FieldMapping { get; set; }
    }
}
