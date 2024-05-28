using productivity_hub_api.DTOs.GoogleCalendar;

namespace productivity_hub_api.Service.GoogleService.Calendar
{
    public interface IGoogleCalendarService
    {
        string GetAuthCode();

        Task<GoogleTokenResponse?> GetTokens(string code);

        Task<GoogleTokenResponse?> RefreshAccessTokenAsync(string refreshToken);

        Task<string?> AddEventToGoogleCalendar(int idEvento, GoogleCalendarReqDto googleCalendarReqDto);
    }
}
