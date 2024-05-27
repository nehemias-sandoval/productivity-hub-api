using AutoMapper;
using productivity_hub_api.DTOs.Auth;
using productivity_hub_api.DTOs.Proyecto;
using productivity_hub_api.Models;
using productivity_hub_api.Repository;
using productivity_hub_api.Repository.ProyectoRepository;
using System.Transactions;

namespace productivity_hub_api.Service.ProyectoService
{
    public class ProyectoService : IProyectoService<ProyectoDto, CreateProyectoDto, UpdateProyectoDto>
    {
        private IRepository<Proyecto> _proyectoRepository;
        private IRepository<Tarea> _tareaRepository;
        private IRepository<Subtarea> _subtareaRepository;
        private ProyectoTareaRepository _proyectoTareaRepository;
        private IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;

        public ProyectoService(
            [FromKeyedServices("proyectoRepository")] IRepository<Proyecto> proyectoRepository,
            [FromKeyedServices("tareaRepository")] IRepository<Tarea> tareaRepository,
            [FromKeyedServices("subtareaRepository")] IRepository<Subtarea> subtareaRepository,
            ProyectoTareaRepository proyectoTareaRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _proyectoRepository = proyectoRepository;
            _tareaRepository = tareaRepository;
            _subtareaRepository = subtareaRepository;
            _proyectoTareaRepository = proyectoTareaRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<ProyectoDto>> GetAllAsync(bool? estado)
        {
            var usuarioDto = _httpContextAccessor.HttpContext?.Items["User"] as UsuarioDto;
            var proyectos = await _proyectoRepository.GetAllAsync();

            if (usuarioDto != null)
            {
                if (estado.HasValue)
                    return proyectos.Where(p => p.Estado == estado && p.IdPersona == usuarioDto.Persona.Id).Select(p => _mapper.Map<ProyectoDto>(p));

                else
                    return proyectos.Where(p => p.IdPersona == usuarioDto.Persona.Id).Select(p => _mapper.Map<ProyectoDto>(p));
            }

            return Enumerable.Empty<ProyectoDto>();
        }

        public async Task<ProyectoDto?> GetByIdAsync(int id)
        {
            var usuarioDto = _httpContextAccessor.HttpContext?.Items["User"] as UsuarioDto;
            var proyecto = await _proyectoRepository.GetByIdAsync(id);

            if (proyecto != null && usuarioDto != null && proyecto.IdPersona == usuarioDto.Persona.Id)
            {
                var proyectoDto = _mapper.Map<ProyectoDto>(proyecto);

                return proyectoDto;
            }

            return null;
        }

        public async Task<ProyectoDto?> AddAsync(CreateProyectoDto createProyectoDto)
        {
            var usuarioDto = _httpContextAccessor.HttpContext?.Items["User"] as UsuarioDto;
            var proyecto = _mapper.Map<Proyecto>(createProyectoDto);

            if (usuarioDto != null) proyecto.IdPersona = usuarioDto.Persona.Id;

            await _proyectoRepository.AddAsync(proyecto);
            await _proyectoRepository.SaveAsync();

            var proyectoDto = _mapper.Map<ProyectoDto>(proyecto);
            return proyectoDto;
        }

        public async Task<ProyectoDto?> UpdateAsync(int id, UpdateProyectoDto updateProyectoDto)
        {
            var usuarioDto = _httpContextAccessor.HttpContext?.Items["User"] as UsuarioDto;
            var proyecto = await _proyectoRepository.GetByIdAsync(id);

            if (proyecto != null && usuarioDto != null && proyecto.IdPersona == usuarioDto.Persona.Id)
            {
                proyecto = _mapper.Map(updateProyectoDto, proyecto);

                _proyectoRepository.Update(proyecto);
                await _proyectoRepository.SaveAsync();

                var proyectoDto = _mapper.Map<ProyectoDto>(proyecto);

                return proyectoDto;
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
                    var proyecto = await _proyectoRepository.GetByIdAsync(id);

                    if (proyecto != null && usuarioDto != null && proyecto.IdPersona == usuarioDto.Persona.Id)
                    {
                        var proyectoTareas = proyecto.ProyectoTareas;
                        var tareas = proyectoTareas.Select(pt => pt.Tarea);
                        var subtareas = tareas.SelectMany(st => st.Subtareas);

                        if (proyectoTareas.Count() > 0) _proyectoTareaRepository.Delete(proyectoTareas);
                        
                        if (subtareas.Count() > 0) _subtareaRepository.Delete(subtareas);
                        
                        if (tareas.Count() > 0) _tareaRepository.Delete(tareas);
                       
                        _proyectoRepository.Delete([proyecto]);
                        await _proyectoRepository.SaveAsync();
                    }
                    else
                    {
                        return null;
                    }

                    transaction.Complete();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task ChangeEstadoAsync(int id)
        {
            var proyecto = await _proyectoRepository.GetByIdAsync(id);
            if (proyecto != null)
            {
                var tareasPendientes = proyecto.ProyectoTareas.Select(p => p.Tarea).Where(p => p.IdEtiqueta != 3);
                if (tareasPendientes.Count() == 0)
                {
                    if (proyecto.Estado) return;
                    proyecto.Estado = true;
                }
                else
                {
                    if (!proyecto.Estado) return;
                    proyecto.Estado = false;
                }                 

                _proyectoRepository.Update(proyecto);
                await _proyectoRepository.SaveAsync();
            }
        }
    }
}
