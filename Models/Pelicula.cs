using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using PracticoLaboratorio4.Validators;

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
        [DisplayName("Género")]
        public int GeneroId { get; set; }

        [DisplayName("Género")]
        public Genero Genero { get; set; }

        [Required]
        [DisplayName("Director")]
        public int DirectorId { get; set; }

        public Director Director { get; set; }

        [StringLength(500)]
        [MaxLength(500)]
        public string Resumen { get; set; }

        [Required]
        [DisplayName("Fecha de estreno")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaEstreno { get; set; }

        [MaxLength(1000)]
        public string UrlImagenPortada { get; set; }

        [NotMapped]
        [DisplayName("Portada")]
        [MinFileSize]
        [MaxFileSize(8 * 1024 * 1024)]
        [AllowedExtensions(".jpg", ".jpeg", ".png")]
        public IFormFile ImagenPortada { get; set; }

        [Url]
        [StringLength(1000)]
        [MaxLength(1000)]
        public string Trailer { get; set; }
    }
}
