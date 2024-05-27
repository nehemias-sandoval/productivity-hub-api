using AutoMapper;
using productivity_hub_api.DTOs.Auth;
using productivity_hub_api.DTOs.Evento;
using productivity_hub_api.Models;
using productivity_hub_api.Repository;
using productivity_hub_api.Repository.EventoRepository;
using System.Transactions;

namespace productivity_hub_api.Service.EventoService
{
    public class EventoService : IEventoService<EventoDto, CreateEventoDto, UpdateEventoDto>
    {
        private IRepository<Evento> _eventoRepository;
        private IRepository<Tarea> _tareaRepository;
        private IRepository<Subtarea> _subtareaRepository;
        private EventoTareaRepository _eventoTareaRepository;
        private IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;

        public EventoService(
            [FromKeyedServices("eventoRepository")] IRepository<Evento> eventoRepository,
            [FromKeyedServices("tareaRepository")] IRepository<Tarea> tareaRepository,
            [FromKeyedServices("subtareaRepository")] IRepository<Subtarea> subtareaRepository,
            EventoTareaRepository eventoTareaRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _eventoRepository = eventoRepository;
            _tareaRepository = tareaRepository;
            _subtareaRepository = subtareaRepository;
            _eventoTareaRepository = eventoTareaRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<EventoDto>> GetAllAsync(bool? vencido)
        {
            var usuarioDto = _httpContextAccessor.HttpContext?.Items["User"] as UsuarioDto;
            var eventos = await _eventoRepository.GetAllAsync();

            if (usuarioDto != null)
            {
                if (vencido.HasValue)
                {
                    if (vencido.Value)
                        return eventos.Where(e => e.Fecha < DateTime.Now && e.IdPersona == usuarioDto.Persona.Id).Select(e => _mapper.Map<EventoDto>(e));

                    else
                        return eventos.Where(e => e.Fecha > DateTime.Now && e.IdPersona == usuarioDto.Persona.Id).Select(e => _mapper.Map<EventoDto>(e));
                }
                else
                {
                    return eventos.Where(e => e.IdPersona == usuarioDto.Persona.Id).Select(e => _mapper.Map<EventoDto>(e));
                }
            }

            return Enumerable.Empty<EventoDto>();
        }

        public async Task<EventoDto?> GetByIdAsync(int id)
        {
            var usuarioDto = _httpContextAccessor.HttpContext?.Items["User"] as UsuarioDto;
            var evento = await _eventoRepository.GetByIdAsync(id);

            if (evento != null && usuarioDto != null && evento.IdPersona == usuarioDto.Persona.Id)
            {
                var eventoDto = _mapper.Map<EventoDto>(evento);

                return eventoDto;
            }

            return null;
        }

        public async Task<EventoDto?> AddAsync(CreateEventoDto createEventoDto)
        {
            var usuarioDto = _httpContextAccessor.HttpContext?.Items["User"] as UsuarioDto;
            var evento = _mapper.Map<Evento>(createEventoDto);

            if (usuarioDto != null) evento.IdPersona = usuarioDto.Persona.Id;

            await _eventoRepository.AddAsync(evento);
            await _eventoRepository.SaveAsync();

            var eventoDto = _mapper.Map<EventoDto>(evento);
            return eventoDto;
        }

        public async Task<EventoDto?> UpdateAsync(int id, UpdateEventoDto updateEventoDto)
        {
            var usuarioDto = _httpContextAccessor.HttpContext?.Items["User"] as UsuarioDto;
            var evento = await _eventoRepository.GetByIdAsync(id);

            if (evento != null && usuarioDto != null && evento.IdPersona == usuarioDto.Persona.Id)
            {
                evento = _mapper.Map(updateEventoDto, evento);

                _eventoRepository.Update(evento);
                await _eventoRepository.SaveAsync();

                var eventoDto = _mapper.Map<EventoDto>(evento);

                return eventoDto;
            }

            return null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            var usuarioDto = _httpContextAccessor.HttpContext?.Items["User"] as UsuarioDto;

            try
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TransactionManager.DefaultTimeout
                };

                using (var transaction = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var evento = await _eventoRepository.GetByIdAsync(id);

                    if (evento != null && usuarioDto != null && evento.IdPersona == usuarioDto.Persona.Id)
                    {
                        var eventoTareas = evento.EventoTareas;
                        var tareas = eventoTareas.Select(et => et.Tarea);
                        var subtareas = tareas.SelectMany(st => st.Subtareas);

                        if (eventoTareas.Count() > 0) _eventoTareaRepository.Delete(eventoTareas);

                        if (subtareas.Count() > 0) _subtareaRepository.Delete(subtareas);

                        if (tareas.Count() > 0) _tareaRepository.Delete(tareas);

                        _eventoRepository.Delete([evento]);
                        await _eventoRepository.SaveAsync();
                    }
                    else
                    {
                        return null;
                    }

                    transaction.Complete();                   
                }

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
