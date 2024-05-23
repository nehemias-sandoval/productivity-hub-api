using productivity_hub_api.DTOs.Subtarea;
using System.ComponentModel.DataAnnotations;

namespace productivity_hub_api.DTOs.Tarea
{
    public class CreateTareaDto
    {
        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaLimite { get; set; }

        public int IdProyectoOrEvento { get; set; }

        public bool IsProyecto { get; set; }

        public int IdPrioridad { get; set; }

        public List<CreateSubtareaDto> Subtareas { get; set; }
    }
}
