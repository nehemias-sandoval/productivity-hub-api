using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Newtonsoft.Json;
using productivity_hub_api.DTOs.GoogleCalendar;
using productivity_hub_api.Settings;
using System.Text;
using productivity_hub_api.Models;
using productivity_hub_api.Repository;

namespace productivity_hub_api.Service.GoogleService.Calendar
{
    public class GoogleCalendarService : IGoogleCalendarService
    {
        private readonly IGoogleCalendarSettings _settings;
        private readonly HttpClient _httpClient;
        private IRepository<Evento> _eventoRepository;

        public GoogleCalendarService(
            IGoogleCalendarSettings settings,
            [FromKeyedServices("eventoRepository")] IRepository<Evento> eventoRepository)
        {
            _settings = settings;
            _eventoRepository = eventoRepository;
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

        public async Task<GoogleTokenResponse?> RefreshAccessTokenAsync(string refreshToken)
        {
            string clientId = _settings.ClientId;
            string clientSecret = _settings.ClientSecret;
            var tokenEndpoint = "https://accounts.google.com/o/oauth2/token";

            var content = new StringContent($"refresh_token={refreshToken}&client_id={clientId}&client_secret={clientSecret}&grant_type=refresh_token", Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await _httpClient.PostAsync(tokenEndpoint, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = JsonConvert.DeserializeObject<GoogleTokenResponse>(responseContent);
                return tokenResponse;
            }

            return null;
        }

        public async Task<string?> AddEventToGoogleCalendar(int idEvento, GoogleCalendarReqDto googleCalendarReqDto)
        {
            try
            {
                var evento = await _eventoRepository.GetByIdAsync(idEvento);

                if (evento != null)
                {
                    var credentials = new UserCredential(
                      new GoogleAuthorizationCodeFlow(
                          new GoogleAuthorizationCodeFlow.Initializer
                          {
                              ClientSecrets = new ClientSecrets
                              {
                                  ClientId = _settings.ClientId,
                                  ClientSecret = _settings.ClientSecret
                              }
                          }),
                      _settings.User,
                      new TokenResponse
                      {
                          RefreshToken = googleCalendarReqDto.RefreshToken
                      });

                    var service = new CalendarService(new BaseClientService.Initializer
                    {
                        HttpClientInitializer = credentials
                    });

                    var newEvent = new Event
                    {
                        Summary = evento.Titulo,
                        Description = evento.Descripcion,
                        Start = new EventDateTime
                        {
                            DateTimeDateTimeOffset = evento.FechaInicio,
                        },
                        End = new EventDateTime
                        {
                            DateTimeDateTimeOffset = evento.FechaFin
                        },
                    };

                    // Usa "primary" como el CalendarId para el calendario principal
                    var insertRequest = service.Events.Insert(newEvent, _settings.CalendarId);
                    var createdEvent = await insertRequest.ExecuteAsync();
                    return createdEvent.Id;
                }

                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
