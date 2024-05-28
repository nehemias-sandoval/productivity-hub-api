using Microsoft.AspNetCore.Mvc;
using productivity_hub_api.DTOs.GoogleCalendar;
using productivity_hub_api.Helpers;
using productivity_hub_api.Service.GoogleService.Calendar;

namespace productivity_hub_api.Controllers
{
    [Route("api/v1/google-calendar")]
    [ApiController]
    [Authorize]
    public class GoogleCalendarController : Controller
    {
        private IGoogleCalendarService _googleCalendarService;

        public GoogleCalendarController(IGoogleCalendarService googleCalendarService)
        {
            _googleCalendarService = googleCalendarService;
        }

        [HttpGet("auth")]
        public ActionResult GoogleAuth()
        {
            string authCode = _googleCalendarService.GetAuthCode();          
            return Ok(new { url = authCode });
        }

        [HttpGet("auth/token/{code}")]
        public async Task<ActionResult> GetTokens(string code)
        {
            var tokens = await _googleCalendarService.GetTokens(code);
            if (tokens == null) return StatusCode(500);
            return Ok(tokens);
        }

        [HttpPost("evento/{idEvento}")]
        public async Task<ActionResult> AddEventToGoogleCalendar(int idEvento, GoogleCalendarReqDto googleCalendarReqDto)
        {
            var eventId = await _googleCalendarService.AddEventToGoogleCalendar(idEvento, googleCalendarReqDto);
            if (eventId == "") return NotFound(new { message = "Evento no encontrado" });
            if (eventId == null) return StatusCode(500);
            return Ok(new { eventId });
        }
    }
}
