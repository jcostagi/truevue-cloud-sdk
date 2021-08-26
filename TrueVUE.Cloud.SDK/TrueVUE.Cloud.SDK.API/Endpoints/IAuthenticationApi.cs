using Refit;
using System.Threading.Tasks;
using TrueVUE.Cloud.SDK.API.Requests;

namespace TrueVUE.Cloud.SDK.API.Endpoints
{
    public interface IAuthenticationApi
    {
        [Post("/api/v1/login")]
        Task<ApiResponse<string>> Authenticate([Query] string apikey, [Body] AuthenticateRequest request);
    }
}
