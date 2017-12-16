using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ChefsBook.Auth.Google
{
    public class GoogleClient : IDisposable
    {
        private const string BaseUrl = "https://www.googleapis.com/oauth2/v3/userinfo";
        private readonly HttpClient httpClient;

        public GoogleClient()
        {
            this.httpClient = new HttpClient { BaseAddress = new Uri(BaseUrl) };
        }
        
        public async Task<GoogleUser> GetGoogleUser(string accessToken)
        {
            var uri = $"?access_token={accessToken}";

            try
            {
                using (var response = await httpClient.GetAsync(uri))
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception($"Could not call Google API. Status code: {response.StatusCode}");

                    var content = await response.Content.ReadAsStringAsync();
                    var result = JObject.Parse(content);

                    if (result["error"] != null)
                        throw new Exception($"Could not call Google API. Code: {result["error"]}, error: {result["error_description"]}");

                    return CreateGoogleUser(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Could not call Google API.");
            }
        }

        private GoogleUser CreateGoogleUser(JObject @object)
        {
            var id = @object["sub"].Value<string>();
            var email = @object["email"]?.Value<string>();
            var firstName = @object["given_name"]?.Value<string>();
            var lastName = @object["last_name"]?.Value<string>();
            var photoUrl = @object["picture"]?.Value<string>();
            return new GoogleUser(id, email, firstName, lastName, photoUrl);
        }

        public void Dispose()
        {
            httpClient?.Dispose();
        }
    }
}