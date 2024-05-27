namespace productivity_hub_api.DTOs.GoogleCalendar
{
    public class GoogleTokenResponse
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public string TokenType { get; set; }

        public int ExpiresIn { get; set; }
    }
}
