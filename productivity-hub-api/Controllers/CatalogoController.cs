using Microsoft.AspNetCore.Mvc;
using productivity_hub_api.DTOs.Catalogo;
using productivity_hub_api.helpers;
using productivity_hub_api.Models;
using productivity_hub_api.Repository.CatalogoRepository;
using productivity_hub_api.Service.CatalogoService;

namespace productivity_hub_api.Controllers
{
    [Route("api/catalogos")]
    [ApiController]
    [Authorize]
    public class CatalogoController : ControllerBase
    {
        private ICatalogoService<EtiquetaDto, FrecuenciaDto, PrioridadDto, TipoEventoDto, TipoNotificacionDto> _catalogoService;

        public CatalogoController(
            [FromKeyedServices("catalogoService")] ICatalogoService<EtiquetaDto, FrecuenciaDto, PrioridadDto, TipoEventoDto, TipoNotificacionDto> catalogoService)
        {
            _catalogoService = catalogoService;
        }

        [HttpGet("frecuencia")]
        public async Task<IEnumerable<FrecuenciaDto>> GetFrecuencias() => await _catalogoService.GetAllFrecuenciasAsync();

        [HttpGet("prioridad")]
        public async Task<IEnumerable<PrioridadDto>> GetPrioridades() => await _catalogoService.GetAllPrioridadesAsync();

        [HttpGet("tipo-evento")]
        public async Task<IEnumerable<TipoEventoDto>> GetTipoEventos() => await _catalogoService.GetAllTipoEventosAsync();

        [HttpGet("tipo-notificacion")]
        public async Task<IEnumerable<TipoNotificacionDto>> GetTipoNotificaciones() => await _catalogoService.GetAllTipoNotificacionAsync();
    }
}
