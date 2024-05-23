using Microsoft.EntityFrameworkCore;
using productivity_hub_api.Models;

namespace productivity_hub_api.Repository
{
    public class CatalogoRepository
    {
        private StoreContext _context;

        public CatalogoRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Frecuencia>> GetAllFrecuenciasAsync() => await _context.Frecuencias.ToListAsync();

        public async Task<IEnumerable<Prioridad>> GetAllPrioridadesAsync() => await _context.Prioridades.ToListAsync();

        public async Task<IEnumerable<TipoEvento>> GetAllTipoEventosAsync() => await _context.TipoEventos.ToListAsync();

        public async Task<IEnumerable<TipoNotificacion>> GetAllTipoNotificacionesAsync() => await _context.TipoNotificaciones.ToListAsync();
    }
}
