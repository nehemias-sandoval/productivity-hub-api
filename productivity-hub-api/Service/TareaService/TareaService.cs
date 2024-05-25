using AutoMapper;
using productivity_hub_api.DTOs.Auth;
using productivity_hub_api.DTOs.Proyecto;
using productivity_hub_api.DTOs.Tarea;
using productivity_hub_api.Models;
using productivity_hub_api.Repository;
using productivity_hub_api.Repository.CatalogoRepository;
using productivity_hub_api.Repository.EventoRepository;
using productivity_hub_api.Repository.ProyectoRepository;
using productivity_hub_api.Service.ProyectoService;

namespace productivity_hub_api.Service.TareaService
{
    public class TareaService : ITareaService<TareaDto, CreateTareaDto, UpdateTareaDto, ChangeEtiquetaTareaDto>
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<Tarea> _tareaRepository;
        private IRepository<Proyecto> _proyectoRepository;
        private IRepository<Evento> _eventoRepository;
        private ProyectoTareaRepository _proyectoTareaRepository;
        private EventoTareaRepository _eventoTareaRepository;
        private CatalogoRepository _catalogoRepository;
        private IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        private IProyectoService<ProyectoDto, CreateProyectoDto, UpdateProyectoDto> _proyectoService;

        public TareaService(
            IUnitOfWork unitOfWork,
            [FromKeyedServices("tareaRepository")] IRepository<Tarea> tareaRepository,
            [FromKeyedServices("proyectoRepository")] IRepository<Proyecto> proyectoRepository,
            [FromKeyedServices("eventoRepository")] IRepository<Evento> eventoRepository,
            ProyectoTareaRepository proyectoTareaRepository,
            EventoTareaRepository eventoTareaRepository,
            CatalogoRepository catalogoRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            [FromKeyedServices("proyectoService")] IProyectoService<ProyectoDto, CreateProyectoDto, UpdateProyectoDto> proyectoService)
        {
            _unitOfWork = unitOfWork;
            _tareaRepository = tareaRepository;
            _mapper = mapper;
            _proyectoRepository = proyectoRepository;
            _eventoRepository = eventoRepository;
            _proyectoTareaRepository = proyectoTareaRepository;
            _eventoTareaRepository = eventoTareaRepository;
            _catalogoRepository = catalogoRepository;
            _httpContextAccessor = httpContextAccessor;
            _proyectoService = proyectoService;
        }

        public async Task<IEnumerable<TareaDto>> GetAllAsync(int? idEtiqueta)
        {
            var usuarioDto = _httpContextAccessor.HttpContext?.Items["User"] as UsuarioDto;

            var tareas = await _tareaRepository.GetAllAsync();

            if (usuarioDto != null)
            {
                if (idEtiqueta.HasValue)
                    return tareas.Where(t => t.IdEtiqueta == idEtiqueta && t.IdPersona == usuarioDto.Persona.Id).Select(t => _mapper.Map<TareaDto>(t));
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

        public async Task<TareaDto?> AddAsync(CreateTareaDto createTareaDto)
        {
            var usuarioDto = _httpContextAccessor.HttpContext?.Items["User"] as UsuarioDto;
            var tarea = _mapper.Map<Tarea>(createTareaDto);

            if (usuarioDto != null)
            {
                tarea.IdPersona = usuarioDto.Persona.Id;
            }

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                await _tareaRepository.AddAsync(tarea);
                await _unitOfWork.SaveChangesAsync();

                if (createTareaDto.IsProyecto)
                {
                    var proyecto = await _proyectoRepository.GetByIdAsync(createTareaDto.IdProyectoOrEvento);
                    if (proyecto != null)
                    {
                        var proyectoTarea = new ProyectoTarea()
                        {
                            IdProyecto = proyecto.Id,
                            IdTarea = tarea.Id,
                        };

                        await _proyectoTareaRepository.AddAsync(proyectoTarea);
                    }
                }
                else
                {
                    var evento = await _eventoRepository.GetByIdAsync(createTareaDto.IdProyectoOrEvento);
                    if (evento != null)
                    {
                        var eventoTarea = new EventoTarea()
                        {
                            IdEvento = evento.Id,
                            IdTarea = tarea.Id,
                        };

                        await _eventoTareaRepository.AddAsync(eventoTarea);
                    }
                }

                await _proyectoService.ChangeEstadoAsync(tarea.ProyectoTareas.Select(pt => pt.Proyecto).First().Id);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                var tareaDto = _mapper.Map<TareaDto>(tarea);
                return tareaDto;

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return null;
            }
        }

        public async Task<TareaDto?> UpdateAsync(int id, UpdateTareaDto updateTareaDto)
        {
            var usuarioDto = _httpContextAccessor.HttpContext?.Items["User"] as UsuarioDto;
            var tarea = await _tareaRepository.GetByIdAsync(id);

            if (tarea != null && usuarioDto != null && tarea.IdPersona == usuarioDto.Persona.Id)
            {
                tarea = _mapper.Map(updateTareaDto, tarea);

                _tareaRepository.Update(tarea);
                await _unitOfWork.SaveChangesAsync();

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
                await _unitOfWork.SaveChangesAsync();

                return tareaDto;
            }

            return null;
        }

        public async Task<TareaDto?> ChangeEtiquetaAsync(int id, ChangeEtiquetaTareaDto changeEtiquetaTareaDto)
        {
            var tarea = await _tareaRepository.GetByIdAsync(id);
            var etiqueta = await _catalogoRepository.GetEtiquetaByIdAsync(changeEtiquetaTareaDto.IdEtiqueta);

            if (tarea != null && etiqueta != null)
            {
                tarea = _mapper.Map<Tarea>(tarea);
                tarea.IdEtiqueta = changeEtiquetaTareaDto.IdEtiqueta;

                _tareaRepository.Update(tarea);
                await _unitOfWork.SaveChangesAsync();

                var tareaDto = _mapper.Map<TareaDto>(tarea);

                return tareaDto;
            }

            return null;
        }

        public async Task CompletarWhenSubtareasAreCompletadasAsync(int id)
        {
            var tarea = await _tareaRepository.GetByIdAsync(id);
            if (tarea != null)
            {
                var subtareasPendientes = tarea.Subtareas.Where(st => !st.Estado);

                if (subtareasPendientes.Count() == 0)
                {
                    if (tarea.IdEtiqueta == 3) return;
                    tarea.IdEtiqueta = 3;
                }
                else
                {
                    if (tarea.IdEtiqueta != 3) return;
                    tarea.IdEtiqueta = 2;
                }

                _tareaRepository.Update(tarea);
                await _unitOfWork.SaveChangesAsync();
            }   
        }
    }
}
