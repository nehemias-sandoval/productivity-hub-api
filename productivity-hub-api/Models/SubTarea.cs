using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace productivity_hub_api.Models
{
    public class SubTarea
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Titulo { get; set; }

        public bool Estado { get; set; }

        public int IdTarea { get; set; }

        [ForeignKey("IdTarea")]
        public Tarea Tarea { get; set; }
    }
}
