using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoClub.Models
{
    [Table("Géneros")]
    public class Generos {

    public enum EstadoGenero
    {
        Activo = 1,
        Inactivo = 0
    }

    
    [Key] 
    public int Id { get; set; }

    [MaxLength(100)]
    public string? Descripcion { get; set; }

    public EstadoGenero Estado { get; set; }
}

}