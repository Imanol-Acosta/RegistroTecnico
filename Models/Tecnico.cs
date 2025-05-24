using System.ComponentModel.DataAnnotations;
using RegistroTecnico;

namespace RegistroTecnico.Models; 

    public class Tecnico

    {
        [Key]

        public int TecnicoId { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string? Nombres { get; set; } = null!;

        public float SueldoHora { get; set; } = 0;
    }




