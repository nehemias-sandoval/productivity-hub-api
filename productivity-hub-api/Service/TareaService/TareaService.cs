using AutoMapper;
using productivity_hub_api.DTOs.Auth;
using productivity_hub_api.DTOs.Proyecto;
using productivity_hub_api.DTOs.Tarea;
using productivity_hub_api.Models;
using productivity_hub_api.Repository;
using productivity_hub_api.Repository.CatalogoRepository;
using productivity_hub_api.Repository.EventoRepository;
using productivity_hub_api.Repository.ProyectoRepository;
using productivity_hub_api.Repository.TareaRepository;
using productivity_hub_api.Service.ProyectoService;
using System.Threading;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace productivity_hub_api.Service.TareaService
{
    public class TareaService : ITareaService<TareaDto, CreateTareaDto, UpdateTareaDto, ChangeEtiquetaTareaDto>
    {
        private IRepository<Tarea> _tareaRepository;
        private IRepository<Subtarea> _subtareaRepository;
        private IRepository<Proyecto> _proyectoRepository;
        private IRepository<Evento> _eventoRepository;
        private ProyectoTareaRepository _proyectoTareaRepository;
        private EventoTareaRepository _eventoTareaRepository;
        private CatalogoRepository _catalogoRepository;
        private IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        private IProyectoService<ProyectoDto, CreateProyectoDto, UpdateProyectoDto> _proyectoService;

        public TareaService(
            [FromKeyedServices("tareaRepository")] IRepository<Tarea> tareaRepository,
            [FromKeyedServices("subtareaRepository")] IRepository<Subtarea> subtareaRepository,
            [FromKeyedServices("proyectoRepository")] IRepository<Proyecto> proyectoRepository,
            [FromKeyedServices("eventoRepository")] IRepository<Evento> eventoRepository,
            ProyectoTareaRepository proyectoTareaRepository,
            EventoTareaRepository eventoTareaRepository,
            CatalogoRepository catalogoRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            [FromKeyedServices("proyectoService")] IProyectoService<ProyectoDto, CreateProyectoDto, UpdateProyectoDto> proyectoService)
        {
            _tareaRepository = tareaRepository;
            _subtareaRepository = subtareaRepository;
            _mapper = mapper;
            _proyectoRepository = proyectoRepository;
            _eventoRepository = eventoRepository;
            _proyectoTareaRepository = proyectoTareaRepository;
            _eventoTareaRepository = eventoTareaRepository;
            _catalogoRepository = catalogoRepository;
            _httpContextAccessor = httpContextAccessor;
            _proyectoService = proyectoService;
        }

        public async Task<IEnumerable<TareaDto>> GetAllAsync(int? idEtiqueta, int? idProyectoOrEvento, bool? isProyecto, DateTime? fecha)
        {
            var usuarioDto = _httpContextAccessor.HttpContext?.Items["User"] as UsuarioDto;
            if (usuarioDto == null) return Enumerable.Empty<TareaDto>();

            var tareasQuery = _tareaRepository.GetAllAsync()
                .ContinueWith(task => task.Result.Where(t => t.IdPersona == usuarioDto.Persona.Id));

            if (idEtiqueta.HasValue)
                tareasQuery = tareasQuery.ContinueWith(task => task.Result.Where(t => t.IdEtiqueta == idEtiqueta.Value));

            if (idProyectoOrEvento.HasValue)
            {
                if (isProyecto.HasValue && isProyecto.Value)
                {
                    tareasQuery = tareasQuery.ContinueWith(task => task.Result.Where(t => t.ProyectoTareas.Any(pt => pt.IdProyecto == idProyectoOrEvento.Value)));
                }
                else
                {
                    tareasQuery = tareasQuery.ContinueWith(task => task.Result.Where(t => t.EventoTareas.Any(et => et.IdEvento == idProyectoOrEvento.Value)));
                }
            }

            if (fecha.HasValue)
            {
                tareasQuery = tareasQuery.ContinueWith(task => task.Result.Where(t => t.FechaLimite == fecha.Value));
            }

            var tareas = await tareasQuery;
            return tareas.Select(t => _mapper.Map<TareaDto>(t));
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

            try
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TransactionManager.DefaultTimeout
                };

                using (var transaction = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    await _tareaRepository.AddAsync(tarea);
                    await _tareaRepository.SaveAsync();

                    if (createTareaDto.IdProyecto.HasValue)
                    {
                        var proyecto = await _proyectoRepository.GetByIdAsync(createTareaDto.IdProyecto.Value);
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

                    if (createTareaDto.IdEvento.HasValue)
                    {
                        var evento = await _eventoRepository.GetByIdAsync(createTareaDto.IdEvento.Value);
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

                    await _tareaRepository.SaveAsync();
                    transaction.Complete();
                }

                if (createTareaDto.IdProyecto.HasValue)
                    await _proyectoService.ChangeEstadoAsync(createTareaDto.IdProyecto.Value);
                
                var tareaDto = _mapper.Map<TareaDto>(tarea);
                return tareaDto;

            }
            catch (Exception)
            {
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
                await _tareaRepository.SaveAsync();

                var tareaDto = _mapper.Map<TareaDto>(tarea);

                return tareaDto;
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

                int? idProyecto = null;

                using (var transaction = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var tarea = await _tareaRepository.GetByIdAsync(id);

                    if (tarea != null && usuarioDto != null && tarea.IdPersona == usuarioDto.Persona.Id)
                    {
                        var proyectoTareas = tarea.ProyectoTareas;
                        var eventosTareas = tarea.EventoTareas;
                        var subtareas = tarea.Subtareas;

                        if (proyectoTareas.Count() > 0)
                        {
                            idProyecto = proyectoTareas.Select(pt => pt.IdProyecto).FirstOrDefault();
                            _proyectoTareaRepository.Delete(proyectoTareas);
                            await _proyectoTareaRepository.SaveAsync();
                        }

                        if (eventosTareas.Count() > 0)
                        {
                            _eventoTareaRepository.Delete(eventosTareas);
                            await _eventoTareaRepository.SaveAsync();
                        }

                        if (subtareas.Count() > 0)
                        {
                            _subtareaRepository.Delete(subtareas);
                            await _subtareaRepository.SaveAsync();
                        }                       

                        _tareaRepository.Delete([tarea]);
                        await _tareaRepository.SaveAsync();
                    }
                    else
                    {
                        return null;
                    }

                    transaction.Complete(); 
                }

                if (idProyecto.HasValue) await _proyectoService.ChangeEstadoAsync(idProyecto.Value);

                return true;
            }
            catch (Exception)
            {
                return false;
            }

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
                await _tareaRepository.SaveAsync();

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
                await _tareaRepository.SaveAsync();
            }   
        }
    }
}
