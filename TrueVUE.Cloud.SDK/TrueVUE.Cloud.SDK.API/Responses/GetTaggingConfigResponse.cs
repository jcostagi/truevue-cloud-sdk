using System.Collections.Generic;

namespace TrueVUE.Cloud.SDK.API.Responses
{
    public class GetTaggingConfigResponse
    {
        public EpcConfigurationResponse EpcConfiguration { get; set; }
        public List<RelatedProductRuleResponse> RelatedProductRules { get; set; }
        public TagConfigurationResponse TagConfiguration { get; set; }
    }
}
