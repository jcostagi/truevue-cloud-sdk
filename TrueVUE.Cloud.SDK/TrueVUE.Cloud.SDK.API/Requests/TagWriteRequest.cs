using System;

namespace TrueVUE.Cloud.SDK.API.Requests
{
    public class TagWriteRequest
    {
        public string EncodingType{ get; set; }
        public string Epc { get; set; }
        public string ExpireTime { get; set; }
        public string ProductCode { get; set; }
        public bool Rewrite { get; set; }
        public long SerialNumber { get; set; }
        public bool Success { get; set; }
        public string UserId { get; set; }
        public string WriteDate { get; set; }
        public string ZoneId { get; set; }
        public RecipeRequest Recipe { get; set; }
    }
}
