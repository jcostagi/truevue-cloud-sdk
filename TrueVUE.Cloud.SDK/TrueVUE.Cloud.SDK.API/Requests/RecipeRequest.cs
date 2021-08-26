using System;
using System.Collections.Generic;
using System.Text;

namespace TrueVUE.Cloud.SDK.API.Requests
{
    public class RecipeRequest
    {
        public DateTime ApplyTime { get; set; }
        public Guid DenormalizedRecipeId { get; set; }
    }
}
