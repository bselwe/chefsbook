using ChefsBook_UWP_App.Services.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace ChefsBook_UWP_App.Services
{
    public class GoogleAuthService
    {
        private const string clientID = "390305502758-7dq0tf86abnvn5v8e55g3ne3shfjrjlb.apps.googleusercontent.com";
        private const string redirectURI = "urn:ietf:wg:oauth:2.0:oob";
        private const string authorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
        private const string authorizationCompleteEndPoint = "https://accounts.google.com/o/oauth2/approval";

        private const string tokenEndpoint = "https://www.googleapis.com/oauth2/v4/token";
        private const string userInfoEndpoint = "https://www.googleapis.com/oauth2/v3/userinfo";

        public async Task<string> GetUserAccessToken()
        {
            string codeVerifier = GenerateRandomDataInBase64url(32);

            var code = await GetAuthorizationCode(codeVerifier);
            var accessToken = await GetUserAccessToken(code, codeVerifier);

            return accessToken;
        }

        public async Task<string> GetUserName(string accessToken)
        {
            var userInfo = await GetUserInfo(accessToken);

            return userInfo.Name;
        }

        private async Task<string> GetAuthorizationCode(string codeVerifier)
        {
            string state = GenerateRandomDataInBase64url(32);
            string codeChallange = EncodeInBase64url(HashWithSha256(codeVerifier));
            const string codeChallangeMethod = "S256";
            
            string authorizationRequest = string.Format("{0}?response_type=code&scope=openid%20profile%20email" +
                "&redirect_uri={1}&client_id={2}&state={3}&code_challenge={4}&code_challenge_method={5}",
                authorizationEndpoint,
                Uri.EscapeDataString(redirectURI),
                clientID,
                state,
                codeChallange,
                codeChallangeMethod
                );
            
            var result = await WebAuthenticationBroker.AuthenticateAsync(
                WebAuthenticationOptions.UseTitle, new Uri(authorizationRequest), new Uri(authorizationCompleteEndPoint)
                );

            var authorizationCode = string.Empty;
            switch (result.ResponseStatus)
            {
                case WebAuthenticationStatus.Success:
                    string data = result.ResponseData;
                    authorizationCode = ProcessAuthorizationResponse(data.Substring(data.IndexOf(' ') + 1), state, codeVerifier);
                    break;

                case WebAuthenticationStatus.ErrorHttp:
                    Print("HTTP error: " + result.ResponseErrorDetail);
                    break;

                case WebAuthenticationStatus.UserCancel:
                    break;
            }

            return authorizationCode;
        }
        
        private string ProcessAuthorizationResponse(string data, string expectedState, string codeVerifier)
        {
            // Parses URI params into a dictionary
            // ref: http://stackoverflow.com/a/11957114/72176
            var queryStringParams = data.Split('&').ToDictionary(
                c => c.Split('=')[0],
                c => Uri.UnescapeDataString(c.Split('=')[1])
            );

            if (queryStringParams.ContainsKey("error"))
            {
                Print(String.Format("OAuth authorization error: {0}.", queryStringParams["error"]));
                return string.Empty;
            }

            if (!queryStringParams.ContainsKey("code") || !queryStringParams.ContainsKey("state"))
            {
                Print("Malformed authorization response. " + data);
                return string.Empty;
            }
            
            string authorizationCode = queryStringParams["code"];
            string incomingState = queryStringParams["state"];   
            if (incomingState != expectedState)
            {
                Print(String.Format("Received request with invalid state ({0})", incomingState));
                return string.Empty;
            }

            return authorizationCode;
        }

        private async Task<string> GetUserAccessToken(string code, string codeVerifier)
        {
            string tokenRequestBody = string.Format("scope=&grant_type=authorization_code" +
                "&code={0}&redirect_uri={1}&client_id={2}&code_verifier={3}",
                code,
                Uri.EscapeDataString(redirectURI),
                clientID,
                codeVerifier
            );
            var content = new StringContent(tokenRequestBody, Encoding.UTF8, "application/x-www-form-urlencoded");
            
            var handler = new HttpClientHandler() { AllowAutoRedirect = true};
            var responseData = string.Empty;
            using (var client = new HttpClient(handler))
            {
                var response = await client.PostAsync(tokenEndpoint, content);
                responseData = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    Print("Authorization code exchange failed.");
                    return string.Empty;
                }
            }
            Print(responseData);
            var token = JsonConvert.DeserializeObject<TokensResponse>(responseData);

            return token.AccessToken;
        }

        private async Task<UserInfo> GetUserInfo(string accessToken)
        {
            var data = string.Empty;
            using (var client = CreateHttpClientWithAuthentication(accessToken))
            {
                var response = await client.GetAsync(userInfoEndpoint);
                data = await response.Content.ReadAsStringAsync();
            }
            Print(data);
            var userInfo = JsonConvert.DeserializeObject<UserInfo>(data);
            
            return userInfo;
        }

        private HttpClient CreateHttpClientWithAuthentication(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            return client;
        }

        #region Helpers

        private void Print(string data)
        {
            Debug.WriteLine(data);
        }

        private static string GenerateRandomDataInBase64url(uint length)
        {
            IBuffer buffer = CryptographicBuffer.GenerateRandom(length);

            return EncodeInBase64url(buffer);
        }

        private static IBuffer HashWithSha256(string inputString)
        {
            var sha = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha256);
            var buff = CryptographicBuffer.ConvertStringToBinary(inputString, BinaryStringEncoding.Utf8);

            return sha.HashData(buff);
        }

        private static string EncodeInBase64url(IBuffer buffer)
        {
            var base64 = CryptographicBuffer.EncodeToBase64String(buffer);
            
            base64 = base64.Replace("+", "-");
            base64 = base64.Replace("/", "_");
            base64 = base64.Replace("=", "");

            return base64;
        }

        #endregion
    }
}
