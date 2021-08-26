using System;

namespace TrueVUE.Cloud.SDK.API.Responses
{
    public class EpcConfigurationResponse
    {
        public int? CompanyPrefixLength { get; set; }
        public string[] CompanyPrefixes { get; set; }
        public bool EnableFiltering { get; set; }
        public string TagEncodingType { get; set; }
    }
}
