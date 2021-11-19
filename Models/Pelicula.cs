using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace PracticoLaboratorio4.Models
{
    public class Pelicula
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [MaxLength(20)]
        [DisplayName("Título")]
        public string Titulo { get; set; }

        [Required]
        public int GeneroId { get; set; }

        [DisplayName("Género")]
        public Genero Genero { get; set; }

        [Required]
        public int DirectorId { get; set; }

        public Director Director { get; set; }

        [StringLength(500)]
        [MaxLength(500)]
        public string Resumen { get; set; }

        [Required]
        [DisplayName("Fecha de estreno")]
        public DateTime FechaEstreno { get; set; }

        [MaxLength(1000)]
        public string UrlImagenPortada { get; set; }

        [NotMapped]
        [DisplayName("Portada")]
        public IFormFile ImagenPortada { get; set; }

        [Url]
        [StringLength(1000)]
        [MaxLength(1000)]
        public string Trailer { get; set; }
    }
}
