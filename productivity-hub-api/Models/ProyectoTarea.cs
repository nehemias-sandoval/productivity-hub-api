using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace productivity_hub_api.Models
{
    public class ProyectoTarea
    {
        public int IdProyecto { get; set; }

        [ForeignKey("IdProyecto")]
        public Proyecto Proyecto { get; set; }

        public int IdTarea { get; set; }

        [ForeignKey("IdTarea")]
        public Tarea Tarea { get; set; }

        public int IdPrioridad { get; set; }

        [ForeignKey("IdPrioridad")] 
        public Prioridad Prioridad  { get; set; }
    }
}
