using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace PracticoLaboratorio4.Models
{
    public class Genero
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [MaxLength(20)]
        [DisplayName("Descripción")]
        [Remote(
            action: "ValidateDescripcion",
            controller: "Generos",
            AdditionalFields = nameof(Id),
            ErrorMessage = "Este género ya existe en la base de datos."
        )]
        public string Descripcion { get; set; }

        public List<Pelicula> Peliculas { get; set; }
    }
}
