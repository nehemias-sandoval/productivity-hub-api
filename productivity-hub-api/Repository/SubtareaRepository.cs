using Microsoft.EntityFrameworkCore;
using productivity_hub_api.Models;

namespace productivity_hub_api.Repository
{
    public class SubtareaRepository : IRepository<Subtarea>
    {

        StoreContext _context;

        public SubtareaRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subtarea>> GetAllAsync() => await _context.Subtareas.ToListAsync();

        public async Task<Subtarea> GetByIdAsync(int id) => await _context.Subtareas.FindAsync(id);

        public async Task AddAsync(Subtarea subtarea) => await _context.Subtareas.AddAsync(subtarea);

        public void Update(Subtarea subtarea)
        {
            _context.Attach(subtarea);
            _context.Subtareas.Entry(subtarea).State = EntityState.Modified;
        }

        public void Delete(Subtarea subtarea) => _context.Subtareas.Remove(subtarea);

        public Task SaveAsync() => _context.SaveChangesAsync(); 
    }
}
