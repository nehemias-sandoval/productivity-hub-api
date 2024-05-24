﻿using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using productivity_hub_api.DTOs.Evento;
using productivity_hub_api.DTOs.Tarea;
using productivity_hub_api.Service.EventoService;

namespace productivity_hub_api.Controllers
{
    [Route("api/evento")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private IValidator<CreateEventoDto> _createEventoValidator;
        private IValidator<UpdateEventoDto> _updateEventoValidator;
        private IEventoService<EventoDto, CreateEventoDto, UpdateEventoDto> _eventoService;

        public EventoController(
            IValidator<CreateEventoDto> createEventoValidator,
            IValidator<UpdateEventoDto> updateEventoValidator,
            [FromKeyedServices("eventoService")] IEventoService<EventoDto, CreateEventoDto, UpdateEventoDto> eventoService)
        {
            _eventoService = eventoService;
            _createEventoValidator = createEventoValidator;
            _updateEventoValidator = updateEventoValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<EventoDto>> Get([FromQuery] bool? vencido) => await _eventoService.GetAllAsync(vencido);

        [HttpGet("{id}")]
        public async Task<ActionResult<EventoDto>> GetById(int id)
        {
            var proyetoDto = await _eventoService.GetByIdAsync(id);
            return proyetoDto == null ? NotFound() : Ok(proyetoDto);
        }

        [HttpPost]
        public async Task<ActionResult<EventoDto>> Add(CreateEventoDto createEventoDto)
        {
            var validationResult = await _createEventoValidator.ValidateAsync(createEventoDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var eventoDto = await _eventoService.AddAsync(createEventoDto);
            if (eventoDto == null) return StatusCode(500);

            return CreatedAtAction(nameof(GetById), new { id = eventoDto.Id }, eventoDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EventoDto>> Update(int id, UpdateEventoDto updateEventoDto)
        {
            var validationResult = await _updateEventoValidator.ValidateAsync(updateEventoDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var proyectoDto = await _eventoService.UpdateAsync(id, updateEventoDto);
            return proyectoDto == null ? NotFound() : Ok(proyectoDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<EventoDto>> Delete(int id)
        {
            var proyectoDto = await _eventoService.DeleteAsync(id);
            return proyectoDto == null ? NotFound() : Ok(proyectoDto);
        }
    }
}
