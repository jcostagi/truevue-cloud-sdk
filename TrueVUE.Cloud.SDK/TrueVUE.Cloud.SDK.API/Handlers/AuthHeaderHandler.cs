using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using TrueVUE.Cloud.SDK.API.Interfaces;

namespace TrueVUE.Cloud.SDK.API.Handlers
{
    public class AuthHeaderHandler : HttpClientHandler, IAuthHeaderHandler
    {
        readonly IAuthTokenService _authTokenService;

        public AuthHeaderHandler(IAuthTokenService authTokenService)
        {
            _authTokenService = authTokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _authTokenService.GetToken();

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
