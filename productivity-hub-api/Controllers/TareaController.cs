using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using productivity_hub_api.DTOs.Auth;
using productivity_hub_api.DTOs.Tarea;
using productivity_hub_api.Helpers;
using productivity_hub_api.Models;
using productivity_hub_api.Repository;
using productivity_hub_api.Repository.CatalogoRepository;
using productivity_hub_api.Service.TareaService;

namespace productivity_hub_api.Controllers
{
    [Route("api/v1/tarea")]
    [ApiController]
    [Authorize]
    public class TareaController : ControllerBase
    {
        private IValidator<CreateTareaDto> _createValidatorDto;
        private IValidator<UpdateTareaDto> _updateValidatorDto;
        private ITareaService<TareaDto, CreateTareaDto, UpdateTareaDto, ChangeEtiquetaTareaDto> _tareaService;
        private IRepository<Proyecto> _proyectoRepository;
        private IRepository<Evento> _eventoRepository;
        private CatalogoRepository _catalogoRepository;
        private IHttpContextAccessor _httpContextAccessor;

        public TareaController(
            IValidator<CreateTareaDto> createValidatorDto,
            IValidator<UpdateTareaDto> updateValidatorDto,
            [FromKeyedServices("tareaService")] ITareaService<TareaDto, CreateTareaDto, UpdateTareaDto, ChangeEtiquetaTareaDto> tareaService,
            [FromKeyedServices("proyectoRepository")] IRepository<Proyecto> proyectoRepository,
            [FromKeyedServices("eventoRepository")] IRepository<Evento> eventoRepository,
            CatalogoRepository catalogoRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _createValidatorDto = createValidatorDto;
            _updateValidatorDto = updateValidatorDto;
            _tareaService = tareaService;
            _proyectoRepository = proyectoRepository;
            _eventoRepository = eventoRepository;
            _catalogoRepository = catalogoRepository;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpGet]
        public async Task<IEnumerable<TareaDto>> Get([FromQuery] int? idEtiqueta) => await _tareaService.GetAllAsync(idEtiqueta);

        [HttpGet("{id}")]
        public async Task<ActionResult<TareaDto>> GetById(int id)
        {
            var tareaDto = await _tareaService.GetByIdAsync(id);
            return tareaDto == null ? NotFound() : Ok(tareaDto);
        }

        [HttpPost]
        public async Task<ActionResult<TareaDto>> Add(CreateTareaDto createTareaDto)
        {
            var usuarioDto = _httpContextAccessor.HttpContext?.Items["User"] as UsuarioDto;
            var validationResult = await _createValidatorDto.ValidateAsync(createTareaDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (createTareaDto.IdProyecto.HasValue)
            {
                var proyecto = await _proyectoRepository.GetByIdAsync(createTareaDto.IdProyecto.Value);
                if (proyecto == null) return NotFound(new { message = "Proyecto no encontrado" });

                if (usuarioDto != null)
                    if (proyecto.IdPersona != usuarioDto.Persona.Id) return Unauthorized(new { message = "El Proyecto no le pertenece" });
            }

            if (createTareaDto.IdEvento.HasValue)
            {
                var evento = await _eventoRepository.GetByIdAsync(createTareaDto.IdEvento.Value);
                if (evento == null) return NotFound(new { message = "Evento no encontrado" });

                if (usuarioDto != null)
                    if (evento.IdPersona != usuarioDto.Persona.Id) return Unauthorized(new { message = "El Evento no le pertenece" });
            }

            var prioridad = await _catalogoRepository.GetPrioridadByIdAsync(createTareaDto.IdPrioridad);
            if (prioridad == null) return NotFound(new { message = "Prioridad no encontrada" });

            var tareaDto = await _tareaService.AddAsync(createTareaDto);
            if (tareaDto == null) return StatusCode(500);

            return CreatedAtAction(nameof(GetById), new { id =  tareaDto.Id }, tareaDto);
        }

        [HttpPut ("{id}")]
        public async Task<ActionResult<TareaDto>> Update(int id, UpdateTareaDto updateTareaDto)
        {
            var validationResult = await _updateValidatorDto.ValidateAsync(updateTareaDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var tareaDto = await _tareaService.UpdateAsync(id, updateTareaDto);
            return tareaDto == null ? NotFound() : Ok(tareaDto);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<TareaDto>> ChangeEtiquetaAsync(ChangeEtiquetaTareaDto changeEtiquetaTareaDto, int id)
        {
            var tareaDto = await _tareaService.ChangeEtiquetaAsync(id, changeEtiquetaTareaDto);
            return tareaDto == null ? NotFound() : Ok(tareaDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TareaDto>> Delete(int id)
        {
            var tareaDto = await _tareaService.DeleteAsync(id);
            return tareaDto == null ? NotFound() : Ok(tareaDto); 
        }
    }
}
