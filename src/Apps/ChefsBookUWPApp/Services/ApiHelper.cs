using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ChefsBook_UWP_App.Services
{
    public class ApiHelper
    {
        private readonly string _baseUrl;
        private readonly string _accessToken;

        public ApiHelper(string baseUrl = "", string accessToken = "")
        {
            _baseUrl = baseUrl;
            _accessToken = accessToken;
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            using (var client = CreateHttpClient())
            {
                var response = await client.GetAsync(endpoint);
                return await HandleResponse<T>(response);
            }
        }

        public async Task PostAsync(string endpoint)
        {
            using (var client = CreateHttpClient())
            {
                var response = await client.PostAsync(endpoint, new JsonStringContent(""));
                HandleResponse(response);
            }
        }

        public async Task PostAsync(string endpoint, object content)
        {
            using (var client = CreateHttpClient())
            {
                var response = await client.PostAsync(endpoint, new JsonStringContent(content));
                HandleResponse(response);
            }
        }

        public async Task<T> PostAsync<T>(string endpoint, object content)
        {
            using (var client = CreateHttpClient())
            {
                var response = await client.PostAsync(endpoint, new JsonStringContent(content));
                return await HandleResponse<T>(response);
            }
        }

        public async Task PutAsync(string endpoint, object content)
        {
            using (var client = CreateHttpClient())
            {
                var response = await client.PutAsync(endpoint, new JsonStringContent(content));
                HandleResponse(response);
            }
        }

        public async Task DeleteAsync(string endpoint)
        {
            using (var client = CreateHttpClient())
            {
                var response = await client.DeleteAsync(endpoint);
                HandleResponse(response);
            }
        }

        #region Helpers

        private void HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                DebugInfo.PrintDebugInfo(response);
            }
        }

        private async Task<T> HandleResponse<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    string json = await response.Content.ReadAsStringAsync();
                    T data = JsonConvert.DeserializeObject<T>(json);
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                DebugInfo.PrintDebugInfo(response);
                return DebugInfo.GetDefault<T>();
            }
        }

        private HttpClient CreateHttpClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl)
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            return client;
        }

        private class JsonStringContent : StringContent
        {
            public JsonStringContent(object obj)
                : base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
            { }
        }

        private class DebugInfo
        {
            public static void PrintDebugInfo(HttpResponseMessage response)
            {
                Debug.WriteLine($"API Error ({response.ReasonPhrase})\n\t" +
                    $"in: {response.RequestMessage.Method} for {response.RequestMessage.RequestUri}");
            }

            public static T GetDefault<T>()
            {
                if (typeof(IEnumerable).IsAssignableFrom(typeof(T)))
                {
                    if (typeof(T).IsGenericType)
                    {
                        Type T_template = typeof(T).GetGenericTypeDefinition();
                        if (T_template == typeof(IEnumerable<>))
                        {
                            return (T)Activator.CreateInstance(typeof(Enumerable).MakeGenericType(typeof(T).GetGenericArguments()));
                        }
                        if (T_template == typeof(IList<>))
                        {
                            return (T)Activator.CreateInstance(typeof(List<>).MakeGenericType(typeof(T).GetGenericArguments()));
                        }
                    }

                    try
                    {
                        return Activator.CreateInstance<T>();
                    }
                    catch (MissingMethodException) { }
                }
                return default(T);
            }
        }

        #endregion
    }
}
