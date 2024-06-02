using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using productivity_hub_api.DTOs.Notificacion;
using productivity_hub_api.Service.NotificacionService;

namespace productivity_hub_api.Controllers
{
    [Route("api/v1/notificacion")]
    [ApiController]
    public class NotificacionController : ControllerBase
    {
        private INotificacionService<NotificacionDto> _notificacionService;

        public NotificacionController([FromKeyedServices("notificacionService")] INotificacionService<NotificacionDto> notificacionService)
        {
            _notificacionService = notificacionService;
        }

        [HttpGet]
        public async Task<IEnumerable<NotificacionDto>> GetNotificaciones() => await _notificacionService.GetAllNotificacionesAsync();
    }
}
