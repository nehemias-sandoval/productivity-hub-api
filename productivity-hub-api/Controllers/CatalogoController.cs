using Microsoft.AspNetCore.Mvc;
using productivity_hub_api.DTOs.Catalogo;
using productivity_hub_api.Helpers;
using productivity_hub_api.Service.CatalogoService;

namespace productivity_hub_api.Controllers
{
    [Route("api/v1/catalogos")]
    [ApiController]
    [Authorize]
    public class CatalogoController : ControllerBase
    {
        private ICatalogoService<EtiquetaDto, PrioridadDto, TipoEventoDto, TipoNotificacionDto> _catalogoService;

        public CatalogoController(
            [FromKeyedServices("catalogoService")] ICatalogoService<EtiquetaDto, PrioridadDto, TipoEventoDto, TipoNotificacionDto> catalogoService)
        {
            _catalogoService = catalogoService;
        }

        [HttpGet("prioridad")]
        public async Task<IEnumerable<PrioridadDto>> GetPrioridades() => await _catalogoService.GetAllPrioridadesAsync();

        [HttpGet("tipo-evento")]
        public async Task<IEnumerable<TipoEventoDto>> GetTipoEventos() => await _catalogoService.GetAllTipoEventosAsync();

        [HttpGet("tipo-notificacion")]
        public async Task<IEnumerable<TipoNotificacionDto>> GetTipoNotificaciones() => await _catalogoService.GetAllTipoNotificacionAsync();
    }
}
