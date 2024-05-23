using AutoMapper;
using productivity_hub_api.DTOs.Auth;
using productivity_hub_api.DTOs.Tarea;
using productivity_hub_api.Models;
using productivity_hub_api.Repository;

namespace productivity_hub_api.Service
{
    public class TareaService : ITareaService<TareaDto, CreateTareaDto, UpdateTareaDto>
    {
        private IRepository<Tarea> _tareaRepository;
        private IMapper _mapper;
        StoreContext _context;
        private IHttpContextAccessor _httpContextAccessor;

        public TareaService([FromKeyedServices("tareaRepository")] IRepository<Tarea> tareaRepository, IMapper mapper, StoreContext context, IHttpContextAccessor httpContextAccessor)
        {
            _tareaRepository = tareaRepository;
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<TareaDto>> GetAllAsync(bool? pendientes)
        {
            var usuarioDto = _httpContextAccessor.HttpContext?.Items["User"] as UsuarioDto;

            var tareas = await _tareaRepository.GetAllAsync();

            if (usuarioDto != null)
            {
                if (pendientes.HasValue)
                    return tareas.Where(t => t.Estado == pendientes.Value && t.IdPersona == usuarioDto.Persona.Id).Select(t => _mapper.Map<TareaDto>(t));
                else
                    return tareas.Where(t => t.IdPersona == usuarioDto.Persona.Id).Select(t => _mapper.Map<TareaDto>(t));
            }

            return Enumerable.Empty<TareaDto>();
        }

        public async Task<TareaDto?> GetByIdAsync(int id)
        {
            var usuarioDto = _httpContextAccessor.HttpContext?.Items["User"] as UsuarioDto;
            var tarea = await _tareaRepository.GetByIdAsync(id);

            if (tarea != null && usuarioDto != null && tarea.IdPersona == usuarioDto.Persona.Id)
            {
                var tareaDto = _mapper.Map<TareaDto>(tarea);

                return tareaDto;
            }

            return null;
        }

        public async Task<TareaDto> AddAsync(CreateTareaDto createTareaDto)
        {
            var usuarioDto = _httpContextAccessor.HttpContext?.Items["User"] as UsuarioDto;
            var tarea = _mapper.Map<Tarea>(createTareaDto);

            if (usuarioDto != null)
            {
                tarea.IdPersona = usuarioDto.Persona.Id;
            }

            await _tareaRepository.AddAsync(tarea);
            await _context.SaveChangesAsync();

            var tareaDto = _mapper.Map<TareaDto>(tarea);

            return tareaDto;
        }

        public async Task<TareaDto?> UpdateAsync(int id, UpdateTareaDto updateTareaDto)
        {
            var usuarioDto = _httpContextAccessor.HttpContext?.Items["User"] as UsuarioDto;
            var tarea = await _tareaRepository.GetByIdAsync(id);

            if(tarea != null && usuarioDto != null && tarea.IdPersona == usuarioDto.Persona.Id)
            {
                tarea = _mapper.Map<UpdateTareaDto, Tarea>(updateTareaDto, tarea);

                _tareaRepository.Update(tarea);
                await _tareaRepository.SaveAsync();

                var tareaDto = _mapper.Map<TareaDto>(tarea);

                return tareaDto;        
            }

            return null;
        }

        public async Task<TareaDto?> DeleteAsync(int id)
        {
            var usuarioDto = _httpContextAccessor.HttpContext?.Items["User"] as UsuarioDto;
            var tarea = await _tareaRepository.GetByIdAsync(id);

            if (tarea != null && usuarioDto != null && tarea.IdPersona == usuarioDto.Persona.Id)
            {
                var tareaDto = _mapper.Map<TareaDto>(tarea);

                _tareaRepository.Delete(tarea);
                await _tareaRepository.SaveAsync();

                return tareaDto;
            }

            return null;
        }

        public async Task<TareaDto?> ChangeStateAsync(int id)
        {
            var tarea = await _tareaRepository.GetByIdAsync(id);

            if(tarea != null)
            {
                tarea = _mapper.Map<Tarea>(tarea);
                tarea.Estado = tarea.Estado ? false : true;

                _tareaRepository.Update(tarea);
                await _tareaRepository.SaveAsync();

                var tareaDto = _mapper.Map<TareaDto>(tarea);

                return tareaDto;
            }

            return null;
        }
    }
}
