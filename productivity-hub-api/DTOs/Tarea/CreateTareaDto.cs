using productivity_hub_api.DTOs.Subtarea;
using System.ComponentModel.DataAnnotations;

namespace productivity_hub_api.DTOs.Tarea
{
    public class CreateTareaDto
    {
        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaLimite { get; set; }

        public List<CreateSubtareaDto> Subtareas { get; set; }
    }
}
