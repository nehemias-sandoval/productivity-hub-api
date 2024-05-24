using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace productivity_hub_api.Models
{
    public class Tarea
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaLimite { get; set; }

        public bool Estado { get; set; }

        public int IdPersona { get; set; }

        [ForeignKey("IdPersona")]
        public Persona Persona { get; set; }

        public int IdPrioridad { get; set; }

        [ForeignKey("IdPrioridad")]
        public Prioridad Prioridad { get; set; }

        public int IdEtiqueta { get; set; }

        [ForeignKey("IdEtiqueta")]
        public Etiqueta Etiqueta { get; set; }

        public List<EventoTarea> EventoTareas { get; set; }

        public List<Subtarea> Subtareas { get; set; }

        public List<ProyectoTarea> ProyectoTareas { get; set; }
    }
}
