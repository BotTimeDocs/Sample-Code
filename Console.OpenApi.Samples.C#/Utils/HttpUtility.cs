using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace OpenApi.Sdk.Utils
{
    public class HttpUtility
    {
        public static void Delete(string url, Dictionary<string, string> headers, string? postData = null)
        {
            Request(HttpMethod.Delete, url, headers, new StringContent(postData ?? "{}"));
        }

        public static T Get<T>(string url, Dictionary<string, string> headers, string? postData = null) where T : class, new()
        {
            return Request<T>(HttpMethod.Get, url, headers, postData);
        }

        public static T Post<T>(string url, Dictionary<string, string> headers, string? postData = null) where T : class, new()
        {
            return Request<T>(HttpMethod.Post, url, headers, postData);
        }

        public static void Put(string url, Dictionary<string, string> headers, Stream stream)
        {
            Request(HttpMethod.Put, url, headers, new StreamContent(stream));
        }

        public static T Put<T>(string url, Dictionary<string, string> headers, Stream stream) where T : class, new()
        {
            return Request<T>(HttpMethod.Put, url, headers, stream);
        }

        public static T Patch<T>(string url, Dictionary<string, string> headers, string? postData = null) where T : class, new()
        {
            return Request<T>(HttpMethod.Patch, url, headers, postData);
        }

        public static T Request<T>(HttpMethod method, string url, Dictionary<string, string> headers, string? postData = null) where T : class, new()
        {
            var resText = Request(method, url, headers, new StringContent(postData ?? "{}"));
            return JsonConvert.DeserializeObject<T>(resText);
        }

        public static T Request<T>(HttpMethod method, string url, Dictionary<string, string> headers, Stream stream) where T : class, new()
        {
            var resText = Request(method, url, headers, new StreamContent(stream));
            return JsonConvert.DeserializeObject<T>(resText);
        }

        private static string Request(HttpMethod method, string url, Dictionary<string, string> headers, HttpContent httpContent)
        {
            var httpclientHandler = new HttpClientHandler()
            {
                UseCookies = false
            };
            using (HttpClient httpClient = new HttpClient(httpclientHandler))
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(method, url);

                httpRequestMessage.Content = httpContent;

                var contentTypeKey = "Content-Type";
                foreach (var header in headers)
                {
                    if (contentTypeKey.Equals(header.Key, System.StringComparison.OrdinalIgnoreCase))
                    {
                        if (httpRequestMessage.Content != null)
                            httpRequestMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(headers[header.Key]);
                    }
                    else
                    {
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                    }
                }

                HttpResponseMessage response = httpClient.SendAsync(httpRequestMessage).Result;

                Task<string> t = response.Content.ReadAsStringAsync();
                string s = t.Result;
                if (!response.IsSuccessStatusCode)
                {
                    
                    throw new System.Exception(s);
                }

                return s;
            }
        }
    }
}