using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KiddyLock.UserInterface.Helpers
{
    public class ApiHelper
    {
        private readonly HttpClient _client;

        public ApiHelper()
        {
            _client = new HttpClient();
        }

        public async Task<T> Get<T>(string uri, string contentType = "application/json", string acceptContentType = "application/json;odata=verbose", HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, Dictionary<string, string> additionalHeaders = null)
        {
            var request = CreateRequest(HttpMethod.Get, uri, contentType, acceptContentType, additionalHeaders);
            var response= await _client.SendAsync(request, completionOption);
            var result = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            return result;
        }

        public Task<HttpResponseMessage> Post(string auth, string uri, string resource = null, object payLoad = null,
            string contentType = "application/json", string acceptContentType = "application/json;odata=verbose",
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, Dictionary<string, string> additionalHeaders = null)
        {
            var request = CreateRequest(HttpMethod.Post, uri, contentType, acceptContentType, additionalHeaders);
            if (payLoad != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(payLoad), Encoding.UTF8,
                    "application/json");
            }

            return _client.SendAsync(request, completionOption);
        }

        private HttpRequestMessage CreateRequest(HttpMethod method, string uri, string contentType = "application/json",
            string acceptContentType = "application/json;odata=verbose", Dictionary<string, string> additionalHeaders = null)
        {
            var request = new HttpRequestMessage(method, uri);
            request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(acceptContentType));
            //request.Headers.Add("Authorization", _tokenHelper.GetToken(resource, auth, appLevelAuth, appLevelTenantId));
            request.Headers.Add("ContentType", contentType);

            if (additionalHeaders == null || additionalHeaders.Count <= 0) return request;

            foreach (var header in additionalHeaders)
            {
                request.Headers.Add(header.Key, header.Value);
            }

            return request;
        }
    }
}
