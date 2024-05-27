using Newtonsoft.Json;
using productivity_hub_api.DTOs.GoogleCalendar;
using productivity_hub_api.Settings;
using System.Text;

namespace productivity_hub_api.Service.GoogleService.Calendar
{
    public class GoogleCalendarService : IGoogleCalendarService
    {
        private readonly IGoogleCalendarSettings _settings;

        private readonly HttpClient _httpClient;

        public GoogleCalendarService(IGoogleCalendarSettings settings)
        {
            _settings = settings;
            _httpClient = new HttpClient();
        }

        public string GetAuthCode()
        {
            string clientId = _settings.ClientId;
            string redirectUrl = _settings.RedirectUri;
            string scope = _settings.Scope[0];
            string authUrl = $"https://accounts.google.com/o/oauth2/auth?redirect_uri={Uri.EscapeDataString(redirectUrl)}&prompt=consent&response_type=code&client_id={clientId}&scope={Uri.EscapeDataString(scope)}&access_type=offline";
            return authUrl;
        }

        public async Task<GoogleTokenResponse?> GetTokens(string code)
        {
            string clientId = _settings.ClientId;
            string clientSecret = _settings.ClientSecret;
            string redirectUrl = _settings.RedirectUri;
            string tokenEndpoint = "https://accounts.google.com/o/oauth2/token";

            var content = new StringContent($"code={code}&redirect_uri={Uri.EscapeDataString(redirectUrl)}&client_id={clientId}&client_secret={clientSecret}&grant_type=authorization_code", Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await _httpClient.PostAsync(tokenEndpoint, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<GoogleTokenResponse>(responseContent);
            }
            else
            {
                return null;
            }
        }

        public Task<string> AddToGoogleCalendar(GoogleCalendarReqDto googleCalendarReqDto)
        {
            throw new NotImplementedException();
        }
    }
}
