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
        readonly IApiOptions _options;

        public AuthHeaderHandler(IAuthTokenService authTokenService, IApiOptions options)
        {
            _authTokenService = authTokenService;
            _options = options;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_options != null && !_options.IsAnonimousRequest)
            {
                var token = _authTokenService.GetToken();

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
            
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}