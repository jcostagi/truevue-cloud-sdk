using Refit;
using System.Net.Http;

namespace TrueVUE.Cloud.SDK.API.Endpoints
{
    public interface IHttpService
    {
        HttpClient HttpClient { get; }
        RefitSettings RefitSettings { get; }
    }
}
