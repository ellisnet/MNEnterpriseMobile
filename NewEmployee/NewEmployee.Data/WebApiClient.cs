using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace NewEmployee.Data {
    public class WebApiClient : IDisposable {

        //See this page for information about implementing a WebAPI client in C# -
        //  http://www.asp.net/web-api/overview/web-api-clients/calling-a-web-api-from-a-net-client

        private HttpClient client;

        public WebApiClient(string apiBaseUrl) {
            client = new HttpClient() {
                BaseAddress = new Uri(apiBaseUrl)
            };
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> GetAsync<T>(string apiName, params string[] apiParams) {
            T result = default(T);

            HttpResponseMessage response = await client.GetAsync(GetApiPath(apiName, apiParams));

            if (response.IsSuccessStatusCode && response.Content != null) {
                //I had trouble with response.Content.ReadAsAsync<T> 
                //  so reading content as string, and using Json.Net to deserialize
                string content = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<T>(content);
            }

            return result;
        }

        public async Task<bool> PostAsync<T>(T data, string apiName, params string[] apiParams) {
            bool result = false;

            HttpContent content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await client.PostAsync(GetApiPath(apiName, apiParams), content);

            if (response.IsSuccessStatusCode)
                result = true;

            return result;
        }

        public async Task<TResult> PostAsync<T, TResult>(T data, string apiName, params string[] apiParams) {
            TResult result = default(TResult);

            HttpContent content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await client.PostAsync(GetApiPath(apiName, apiParams), content);

            if (response.IsSuccessStatusCode && response.Content != null) {
                string responseContent = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<TResult>(responseContent);
            }

            return result;
        }

        private string GetApiPath(string apiName, string[] apiParams = null) {
            string result = "api";

            apiName = (apiName == null) ? "" : apiName.Trim();
            if (apiName != "")
                result += "/" + Uri.EscapeDataString(apiName);
            if (apiParams != null) {
                foreach (string param in apiParams) {
                    if (param != null && param.Trim() != "")
                        result += "/" + Uri.EscapeDataString(param);
                }
            }

            return result;
        }

        public void Dispose() {
            client.Dispose();
            client = null;
        }
    }

}
