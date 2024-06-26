﻿using Microsoft.EntityFrameworkCore;
using productivity_hub_api.Models;

namespace productivity_hub_api.Repository.EventoRepository
{
    public class EventoRepository : IRepository<Evento>
    {
        private StoreContext _context;

        public EventoRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Evento>> GetAllAsync() => await _context.Eventos.Include(e => e.TipoEvento).ToListAsync();

        public async Task<Evento?> GetByIdAsync(int id) => await _context.Eventos
            .Include(e => e.TipoEvento)
            .Include(e => e.EventoTareas)
            .ThenInclude(et => et.Tarea)
            .ThenInclude(t => t.Subtareas)
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync();

        public async Task AddAsync(Evento evento) => await _context.Eventos.AddAsync(evento);

        public void Update(Evento evento)
        {
            _context.Attach(evento);
            _context.Eventos.Entry(evento).State = EntityState.Modified;
        }

        public void Delete(IEnumerable<Evento> eventos) => _context.Eventos.RemoveRange(eventos);

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
