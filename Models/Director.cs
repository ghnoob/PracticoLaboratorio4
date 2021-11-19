using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace PracticoLaboratorio4.Models
{
    public class Director
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [StringLength(500)]
        [MaxLength(500)]
        public string Biografia { get; set; }

        [MaxLength(1000)]
        public string UrlFoto { get; set; }

        [NotMapped]
        public IFormFile Foto { get; set; }

        public List<Pelicula> Peliculas { get; set; }
    }
}
