using Microsoft.EntityFrameworkCore;
using productivity_hub_api.Models;

namespace productivity_hub_api.Repository.ProyectoRepository
{
    public class ProyectoRepository : IRepository<Proyecto>
    {
        private StoreContext _context;

        public ProyectoRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Proyecto>> GetAllAsync() => await _context.Proyectos.ToListAsync();

        public async Task<Proyecto?> GetByIdAsync(int id) =>  await _context.Proyectos.Include(p => p.ProyectoTareas).ThenInclude(pt => pt.Tarea).Where(p => p.Id == id).FirstOrDefaultAsync();

        public async Task AddAsync(Proyecto proyecto) => await _context.Proyectos.AddAsync(proyecto);

        public void Update(Proyecto proyecto)
        {
            _context.Attach(proyecto);
            _context.Proyectos.Entry(proyecto).State = EntityState.Modified;
        }

        public void Delete(Proyecto proyecto) => _context.Proyectos.Remove(proyecto);

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
