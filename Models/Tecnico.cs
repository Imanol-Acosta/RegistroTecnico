using System.ComponentModel.DataAnnotations;
using RegistroTecnico;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;


namespace RegistroTecnico.Models
{
    [Table("tecnicos")] 
    public class Tecnico : BaseModel
    {
        [PrimaryKey("tecnicoid", false)] 
        [Column("tecnicoid")]
        public int TecnicoID { get; set; }

        [Column("nombres")]
        public string Nombres { get; set; } = string.Empty;

        [Column("sueldohora")]
        public float SueldoHora { get; set; } = 0;
    }
}
