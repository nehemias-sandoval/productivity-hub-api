using Microsoft.EntityFrameworkCore;
using productivity_hub_api.DTOs.Auth;
using productivity_hub_api.Models;

namespace productivity_hub_api.Repository.NotificacionRepository
{
    public class NotificacionRepository
    {
        StoreContext _context;
        private IHttpContextAccessor _httpContextAccessor;

        public NotificacionRepository(StoreContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Notificacion>> GetAllNotificacionesAsync()
        {
            var usuarioDto = _httpContextAccessor.HttpContext?.Items["User"] as UsuarioDto;
            if (usuarioDto != null)
            {
                var notificaciones = await _context.Notificaciones.Include(n => n.TipoNotificacion).Where(n => n.IdPersona == usuarioDto.Persona.Id).ToListAsync();
                return notificaciones;
            }

            return Enumerable.Empty<Notificacion>();
        }

        public async Task AddAsync(Notificacion notificacion) => await _context.AddAsync(notificacion);

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
