using System;

namespace TrueVUE.Cloud.SDK.API.Responses
{
    public class GetProductInfoResponse
    {
        public string ProductAttributeValue { get; set; }
        public string DisplayName { get; set; }
        public long ProductCode { get; set; }
        public Guid ProductAttributeId { get; set; }
        public string LanguageCode { get; set; }
    }
}
