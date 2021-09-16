using Refit;
using System;
using System.Net.Http;
using TrueVUE.Cloud.SDK.API.Interfaces;

namespace TrueVUE.Cloud.SDK.API.Services
{
    public class HttpService : IHttpService
    {
        public HttpService(IAuthHeaderHandler authHeaderHandler, IApiOptions options)
        {
            HttpClient = new HttpClient(authHeaderHandler as HttpMessageHandler)
            {
                BaseAddress = options.BusinessUnitUrl == null ? null : new Uri(options.BusinessUnitUrl),
                Timeout = TimeSpan.FromSeconds(15)
            };
        }
        public HttpClient HttpClient { get; }
        public RefitSettings RefitSettings { get; }
    }
}
