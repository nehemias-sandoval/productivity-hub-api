using System.ComponentModel.DataAnnotations.Schema;

namespace productivity_hub_api.Models
{
    public class EventoTarea
    {
        public int IdEvento { get; set; }

        [ForeignKey("IdEvento")]
        public Evento Evento { get; set; }

        public int IdTarea { get; set; }

        [ForeignKey("IdTarea")]
        public Tarea Tarea { get; set; }

        public int IdPrioridad { get; set; }

        [ForeignKey("IdPrioridad")]
        public Prioridad Prioridad { get; set; }
    }
}
