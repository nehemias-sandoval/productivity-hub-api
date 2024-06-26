﻿using productivity_hub_api.DTOs.Tarea;

namespace productivity_hub_api.Service.TareaService
{
    public interface ITareaService<TareaDto, CreateTareaDto, UpdateTareaDto, ChangeEtiquetaTareaDto>
    {
        Task<IEnumerable<TareaDto>> GetAllAsync(int? idEtiqueta, int? idPrioridad, int? idProyectoOrEvento, bool? isProyecto, DateTime? fecha);

        Task<TareaDto?> GetByIdAsync(int id);

        Task<TareaDto?> AddAsync(CreateTareaDto createDto);

        Task<TareaDto?> UpdateAsync(int id, UpdateTareaDto updateDto);

        Task<bool?> DeleteAsync(int id);

        Task<TareaDto?> ChangeEtiquetaAsync(int id, ChangeEtiquetaTareaDto changeDto);

        Task CompletarWhenSubtareasAreCompletadasAsync(int id);
    }
}
