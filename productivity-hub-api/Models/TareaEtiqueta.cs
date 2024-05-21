using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace productivity_hub_api.Models
{
    public class TareaEtiqueta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int IdTarea { get; set; }

        [ForeignKey("IdTarea")]
        public Tarea Tarea { get; set; }

        public int IdEtiqueta { get; set; }

        [ForeignKey("IdEtiqueta")]
        public Etiqueta Etiqueta { get; set; }
    }
}
