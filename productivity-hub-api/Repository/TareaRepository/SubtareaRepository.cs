using Microsoft.EntityFrameworkCore;
using productivity_hub_api.DTOs.Auth;
using productivity_hub_api.Models;

namespace productivity_hub_api.Repository.TareaRepository
{
    public class SubtareaRepository : IRepository<Subtarea>
    {
        StoreContext _context;
        private IHttpContextAccessor _httpContextAccessor;

        public SubtareaRepository(StoreContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Subtarea>> GetAllAsync()
        {
            var usuarioDto = _httpContextAccessor.HttpContext?.Items["User"] as UsuarioDto;

            if (usuarioDto != null)
                return await _context.Subtareas.Include(st => st.Tarea).Where(st => st.Tarea.IdPersona == usuarioDto.Persona.Id).ToListAsync();

            return Enumerable.Empty<Subtarea>();
        }

        public async Task<Subtarea?> GetByIdAsync(int id)
        {
            var usuarioDto = _httpContextAccessor.HttpContext?.Items["User"] as UsuarioDto;

            if (usuarioDto != null)
                return await _context.Subtareas.Include(st => st.Tarea).ThenInclude(t => t.ProyectoTareas).ThenInclude(pt => pt.Proyecto).Where(st => st.Id == id && st.Tarea.IdPersona == usuarioDto.Persona.Id).FirstOrDefaultAsync();

            return null;
        }

        public async Task AddAsync(Subtarea subtarea) => await _context.Subtareas.AddAsync(subtarea);

        public void Update(Subtarea subtarea)
        {
            _context.Attach(subtarea);
            _context.Subtareas.Entry(subtarea).State = EntityState.Modified;
        }

        public void Delete(Subtarea subtarea) => _context.Subtareas.Remove(subtarea);
    }
}
