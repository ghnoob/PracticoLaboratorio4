using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PracticoLaboratorio4.Models
{
    public class Genero
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [MaxLength(20)]
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
    }
}
