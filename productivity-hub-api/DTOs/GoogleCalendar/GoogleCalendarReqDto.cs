namespace productivity_hub_api.DTOs.GoogleCalendar
{
    public class GoogleCalendarReqDto
    {
        public string CalendarId { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string RefreshToken { get; set; }
    }
}
