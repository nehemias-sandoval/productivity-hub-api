namespace productivity_hub_api.Settings
{
    public class GoogleCalendarSettings : IGoogleCalendarSettings
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string RedirectUri { get; set; }

        public string[] Scope { get; set; }

        public string ApplicationName { get; set; }

        public string User { get; set; }

        public string CalendarId { get; set; }
    }
}
