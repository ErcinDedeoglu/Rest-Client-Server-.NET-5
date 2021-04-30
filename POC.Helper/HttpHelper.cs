using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace POC.Helper
{
    public class HttpHelper
    {
        private readonly string _apiUrl;

        public HttpHelper(string apiUrl)
        {
            _apiUrl = apiUrl;
        }

        public async Task<T> Post<T>(string url, T contentValue, CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiUrl);
                var content = new StringContent(JsonConvert.SerializeObject(contentValue), Encoding.UTF8,
                    "application/json");
                var result = await client.PostAsync(url, content, cancellationToken);
                result.EnsureSuccessStatusCode();
                
                var resultContentString = await result.Content.ReadAsStringAsync(cancellationToken);
                var resultContent = JsonConvert.DeserializeObject<T>(resultContentString);

                return resultContent;
            }
        }

        public async Task Put<T>(string url, T stringValue, CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiUrl);
                var content = new StringContent(JsonConvert.SerializeObject(stringValue), Encoding.UTF8,
                    "application/json");
                var result = await client.PutAsync(url, content, cancellationToken);
                result.EnsureSuccessStatusCode();
            }
        }

        public async Task<T> Get<T>(string url, CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiUrl);
                var result = await client.GetAsync(url, cancellationToken);
                result.EnsureSuccessStatusCode();
                var resultContentString = await result.Content.ReadAsStringAsync(cancellationToken);
                var resultContent = JsonConvert.DeserializeObject<T>(resultContentString);
                return resultContent;
            }
        }

        public async Task Delete(string url, CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiUrl);
                var result = await client.DeleteAsync(url, cancellationToken);
                result.EnsureSuccessStatusCode();
            }
        }
    }
}