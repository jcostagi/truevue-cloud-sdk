using System.Linq;
using TrueVUE.Cloud.SDK.API.Interfaces;

namespace TrueVUE.Cloud.SDK.API.Services
{
    public class AuthTokenService : IAuthTokenService
    {
        IApiOptions _apiOptions;

        public AuthTokenService(IApiOptions options)
        {
            _apiOptions = options;
        }

        public string GetToken()
        {
            return _apiOptions?.Token;
        }
    }
}
