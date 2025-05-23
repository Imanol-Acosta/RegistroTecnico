using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnico.Models;

    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

    
    public DateTime FechaIngreso { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "El nombre es requerido")]
    [StringLength(100, ErrorMessage ="No se permiten numeros ni simbolos")]
    public string? Nombres { get; set; }

    [Required(ErrorMessage = "La direccion es requerida")]
    public string Direccion {  get; set; } = string.Empty;
        
    [StringLength(13, ErrorMessage = "El RNC debe tener el formato 000-0000000-0")]
    [RegularExpression(@"^\d{3}-\d{7}-\d{1}$", ErrorMessage = "El RNC debe tener el formato 000-0000000-0")]
    public string Rnc { get; set; } = string.Empty;

    [Required(ErrorMessage = "El límite de crédito es requerido")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El límite de crédito debe ser mayor a 0")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal LimiteCredito { get; set; } = 0;

    [Required(ErrorMessage = "Debe seleccionar un técnico encargado")]
    public int TecnicoId { get; set; }

    [ForeignKey("TecnicoId")]
    public virtual Tecnico? Tecnico { get; set; }

}

