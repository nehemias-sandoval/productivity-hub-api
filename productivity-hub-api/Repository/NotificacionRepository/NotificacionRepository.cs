using Microsoft.EntityFrameworkCore;
using productivity_hub_api.Models;

namespace productivity_hub_api.Repository.NotificacionRepository
{
    public class NotificacionRepository
    {
        StoreContext _context;

        public NotificacionRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notificacion>> GetAllNotificacionesAsync () => await _context.Notificaciones.Include(n => n.TipoNotificacion).ToListAsync();

        public async Task AddAsync(Notificacion notificacion) => await _context.AddAsync(notificacion);
    }
}
