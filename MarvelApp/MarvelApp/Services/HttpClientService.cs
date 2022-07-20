using MarvelApp.Helpers;
using MarvelApp.Services.Interfaces;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MarvelApp.Services
{
    public static class HttpClientService
    {
        static HttpClient clientInstance;
        static HttpClient Client => clientInstance ?? 
            (clientInstance = new HttpClient(new HttpClientHandler { AllowAutoRedirect = false, UseCookies = false }));

        public static async Task<T> GetAsync<T>(string url, bool caching = false, bool forceRefresh = false)
        {
            var json = string.Empty;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                json = Barrel.Current.Get<string>(url);
            else if (!forceRefresh && !Barrel.Current.IsExpired(url))
                json = Barrel.Current.Get<string>(url);

            try
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    var uri = GetHashAndSetupUri(url);
                    var response = await Client.GetAsync(uri);

                    await HandleResponse(response);

                    string responseData = await response.Content.ReadAsStringAsync();
                    var jObject = JObject.Parse(responseData);
                    json = jObject["data"]["results"].ToString();

                    if (caching)
                        Barrel.Current.Add(key: url, data: json, expireIn: TimeSpan.FromDays(1));
                }
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (HttpRequestException ex)
            {
                DependencyService.Get<IMessage>().LongAlert("Please check your internet connection");
                throw ex;
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().LongAlert("Unable to get information from server");
                Debug.WriteLine($"Unable to get information from server {ex}");
                throw ex;
            }
        }

        private static Uri GetHashAndSetupUri(string url)
        {
            var ts = DateTime.UtcNow.Ticks;
            var hash = MD5Hash.Hash(ts + ApiConstants.PRIVATE_KEY + ApiConstants.PUBLIC_KEY);

            url += $"{(url.Contains("?") ? "&" : "?")}ts={ts}&apikey={ApiConstants.PUBLIC_KEY}&hash={hash}";

            return new Uri(url);
        }

        private static async Task HandleResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                return;

            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
                throw new Exception(content);

            throw new HttpRequestException(content);
        }
    }
}
