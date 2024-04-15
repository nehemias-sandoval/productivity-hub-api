using System.ComponentModel.DataAnnotations.Schema;

namespace productivity_hub_api.Models
{
    public class EventoRecordatorio
    {
        public int IdEvento { get; set; }

        [ForeignKey("IdEvento")]
        public Evento Evento { get; set; }

        public int IdRecordatorio { get; set; }

        [ForeignKey("IdRecordatorio")]
        public Recordatorio Recordatorio { get; set; }

        public int IdFrecuencia { get; set; }
        
        [ForeignKey("IdFrecuencia")]
        public Frecuencia Frecuencia { get; set; }
    }
}
