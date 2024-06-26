﻿using Microsoft.EntityFrameworkCore;
using productivity_hub_api.Models;
using System.Security.AccessControl;

namespace productivity_hub_api.Repository.TareaRepository
{
    public class TareaRepository : IRepository<Tarea>
    {
        StoreContext _context;

        public TareaRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tarea>> GetAllAsync() => await _context.Tareas
            .Include(t => t.ProyectoTareas).ThenInclude(pt => pt.Proyecto)
            .Include(t => t.EventoTareas).ThenInclude(et => et.Evento).ThenInclude(e => e.TipoEvento)
            .Include(t => t.Persona).ThenInclude(p => p.Usuario)
            .Include(t => t.Etiqueta)
            .Include(t => t.Prioridad)
            .Include(t => t.Subtareas)
            .ToListAsync();

        public async Task<Tarea?> GetByIdAsync(int id) => await _context.Tareas
            .Include(t => t.ProyectoTareas).ThenInclude(pt => pt.Proyecto)
            .Include(t => t.EventoTareas).ThenInclude(et => et.Evento).ThenInclude(e => e.TipoEvento)
            .Include(t => t.Persona)
            .Include(t => t.Etiqueta)
            .Include(t => t.Prioridad)
            .Include(t => t.Subtareas)
            .Where(t => t.Id == id)
            .FirstOrDefaultAsync();

        public async Task AddAsync(Tarea tarea) => await _context.Tareas.AddAsync(tarea);

        public void Update(Tarea tarea)
        {
            _context.Attach(tarea);
            _context.Tareas.Entry(tarea).State = EntityState.Modified;
        }

        public void Delete(IEnumerable<Tarea> tareas) => _context.Tareas.RemoveRange(tareas);

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
