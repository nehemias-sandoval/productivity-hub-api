namespace productivity_hub_api.Settings
{
    public interface IGoogleCalendarSettings
    {
        string ClientId { get; set; }

        string ClientSecret { get; set; }

        string RedirectUri { get; set; }

        string[] Scope { get; set; }

        string ApplicationName { get; set; }

        string User { get; set; }

        string CalendarId { get; }
    }
}
