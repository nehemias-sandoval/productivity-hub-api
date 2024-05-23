using AutoMapper;
using productivity_hub_api.DTOs.Auth;
using productivity_hub_api.DTOs.Evento;
using productivity_hub_api.Models;
using productivity_hub_api.Repository;

namespace productivity_hub_api.Service
{
    public class EventoService : ICommonService<EventoDto, CreateEventoDto, UpdateEventoDto>
    {
        private IRepository<Evento> _eventoRepository;
        private IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;

        public EventoService(
            [FromKeyedServices("eventoRepository")] IRepository<Evento> eventoRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _eventoRepository = eventoRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<EventoDto>> GetAllAsync()
        {
            var usuarioDto = _httpContextAccessor.HttpContext?.Items["User"] as UsuarioDto;

            if (usuarioDto != null)
            {
                var eventos = await _eventoRepository.GetAllAsync();
                return eventos.Where(e => e.IdPersona == usuarioDto.Persona.Id).Select(e => _mapper.Map<EventoDto>(e));
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

        public async Task<EventoDto> AddAsync(CreateEventoDto createEventoDto)
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
                evento = _mapper.Map<UpdateEventoDto, Evento>(updateEventoDto, evento);

                _eventoRepository.Update(evento);
                await _eventoRepository.SaveAsync();

                var eventoDto = _mapper.Map<EventoDto>(evento);

                return eventoDto;
            }

            return null;
        }

        public async Task<EventoDto?> DeleteAsync(int id)
        {
            var usuarioDto = _httpContextAccessor.HttpContext?.Items["User"] as UsuarioDto;
            var evento = await _eventoRepository.GetByIdAsync(id);

            if (evento != null && usuarioDto != null && evento.IdPersona == usuarioDto.Persona.Id)
            {
                var eventoDto = _mapper.Map<EventoDto>(evento);

                _eventoRepository.Delete(evento);
                await _eventoRepository.SaveAsync();

                return eventoDto;
            }

            return null;
        }
    }
}
