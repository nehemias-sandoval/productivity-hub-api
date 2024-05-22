using Microsoft.EntityFrameworkCore;
using productivity_hub_api.Models;
using System.Security.AccessControl;

namespace productivity_hub_api.Repository
{
    public class TareaRepository : IRepository<Tarea>
    {
        StoreContext _context;

        public TareaRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tarea>> GetAllAsync() => await _context.Tareas.Include(t => t.Persona).ToListAsync();

        public async Task<Tarea?> GetByIdAsync(int id) => await _context.Tareas.Include(t => t.Persona).Where(t => t.Id == id).FirstOrDefaultAsync();

        public async Task AddAsync(Tarea tarea) => await _context.Tareas.AddAsync(tarea);

        public void Update(Tarea tarea)
        {
            _context.Attach(tarea);
            _context.Tareas.Entry(tarea).State = EntityState.Modified;
        }

        public void Delete(Tarea tarea) => _context.Tareas.Remove(tarea); 

        public Task SaveAsync() => _context.SaveChangesAsync(); 
    }
}
