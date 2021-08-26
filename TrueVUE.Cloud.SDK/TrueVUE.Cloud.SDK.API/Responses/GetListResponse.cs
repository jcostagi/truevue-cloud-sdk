using System;
using System.Collections.Generic;

namespace TrueVUE.Cloud.SDK.API.Responses
{
    public class GetListResponse<T>
    {
        public IList<T> Content { get; set; }
    }
}
