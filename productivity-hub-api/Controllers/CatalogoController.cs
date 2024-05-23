using Microsoft.AspNetCore.Mvc;
using productivity_hub_api.helpers;
using productivity_hub_api.Models;
using productivity_hub_api.Repository;

namespace productivity_hub_api.Controllers
{
    [Route("api/catalogos")]
    [ApiController]
    [Authorize]
    public class CatalogoController : ControllerBase
    {
        private CatalogoRepository _catalogoRepository;

        public CatalogoController(CatalogoRepository catalogoRepository)
        {
            _catalogoRepository = catalogoRepository;
        }

        [HttpGet("frecuencia")]
        public async Task<IEnumerable<Frecuencia>> GetFrecuencias() => await _catalogoRepository.GetAllFrecuenciasAsync();

        [HttpGet("prioridad")]
        public async Task<IEnumerable<Prioridad>> GetPrioridades() => await _catalogoRepository.GetAllPrioridadesAsync();

        [HttpGet("tipo-evento")]
        public async Task<IEnumerable<TipoEvento>> GetTipoEventos() => await _catalogoRepository.GetAllTipoEventosAsync();

        [HttpGet("tipo-notificacion")]
        public async Task<IEnumerable<TipoNotificacion>> GetTipoNotificaciones() => await _catalogoRepository.GetAllTipoNotificacionesAsync();
    }
}
