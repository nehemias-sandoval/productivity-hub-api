﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using productivity_hub_api.DTOs.Proyecto;
using productivity_hub_api.Helpers;
using productivity_hub_api.Service.ProyectoService;

namespace productivity_hub_api.Controllers
{
    [Route("api/v1/proyecto")]
    [ApiController]
    [Authorize]
    public class ProyectoController : ControllerBase
    {
        private IValidator<CreateProyectoDto> _createProyectoValidator;
        private IValidator<UpdateProyectoDto> _updateProyectoValidator;
        private IProyectoService<ProyectoDto, CreateProyectoDto, UpdateProyectoDto> _proyectoService;

        public ProyectoController(
            IValidator<CreateProyectoDto> createProyectoValidator,
            IValidator<UpdateProyectoDto> updateProyectoValidator,
            [FromKeyedServices("proyectoService")] IProyectoService<ProyectoDto, CreateProyectoDto, UpdateProyectoDto> proyectoService)
        {
            _proyectoService = proyectoService;
            _createProyectoValidator = createProyectoValidator;
            _updateProyectoValidator = updateProyectoValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<ProyectoDto>> Get([FromQuery] bool? estado) => await _proyectoService.GetAllAsync(estado);

        [HttpGet("{id}")]
        public async Task<ActionResult<ProyectoDto>> GetById(int id)
        {
            var proyetoDto = await _proyectoService.GetByIdAsync(id);
            return proyetoDto == null ? NotFound() : Ok(proyetoDto);
        }

        [HttpPost]
        public async Task<ActionResult<ProyectoDto>> Add(CreateProyectoDto createProyectoDto)
        {
            var validationResult = await _createProyectoValidator.ValidateAsync(createProyectoDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var proyectoDto = await _proyectoService.AddAsync(createProyectoDto);
            if (proyectoDto == null) return StatusCode(500);

            return CreatedAtAction(nameof(GetById), new { id = proyectoDto.Id }, proyectoDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProyectoDto>> Update(int id, UpdateProyectoDto updateProyectoDto)
        {
            var validationResult = await _updateProyectoValidator.ValidateAsync(updateProyectoDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var proyectoDto = await _proyectoService.UpdateAsync(id, updateProyectoDto);
            return proyectoDto == null ? NotFound() : Ok(proyectoDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _proyectoService.DeleteAsync(id);
            if (!result.HasValue) return NotFound();

            return result.Value ? Ok() : StatusCode(500);
        }
    }
}
