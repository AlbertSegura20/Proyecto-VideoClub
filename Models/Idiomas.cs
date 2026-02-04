using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoClub.Models
{ 

    [Table("Idiomas")]
    public class Idiomas
    {

    public enum EstadoIdioma
    {
        [Display(Name = "Activo")]
        Activo = 1,
        [Display(Name = "Inactivo")]
        Inactivo = 0
    }

    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string? Descripcion { get; set; }

    public EstadoIdioma Estado { get; set; }
}

}