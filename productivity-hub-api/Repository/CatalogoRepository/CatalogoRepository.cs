using Microsoft.EntityFrameworkCore;
using productivity_hub_api.Models;

namespace productivity_hub_api.Repository.CatalogoRepository
{
    public class CatalogoRepository
    {
        private StoreContext _context;

        public CatalogoRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Prioridad>> GetAllPrioridadesAsync() => await _context.Prioridades.ToListAsync();

        public async Task<Prioridad?> GetPrioridadByIdAsync(int id) => await _context.Prioridades.Where(p => p.Id == id).FirstOrDefaultAsync();

        public async Task<IEnumerable<TipoEvento>> GetAllTipoEventosAsync() => await _context.TipoEventos.ToListAsync();

        public async Task<IEnumerable<TipoNotificacion>> GetAllTipoNotificacionesAsync() => await _context.TipoNotificaciones.ToListAsync();

        public async Task<IEnumerable<Etiqueta>> GetAllTipoEtiquetasAsync() => await _context.Etiquetas.ToListAsync();

        public async Task<Etiqueta?> GetEtiquetaByIdAsync(int id) => await _context.Etiquetas.Where(e => e.Id == id).FirstOrDefaultAsync();
    }
}
