using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoClub.Models
{
    [Table("Elenco")]
    public class Elenco
    {

        public enum EstadoElenco
        {
            [Display(Name = "Activo")]
            Activo = 1,
            [Display(Name = "Inactivo")]
            Inactivo = 0
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Nombre { get; set; }

        public EstadoElenco Estado { get; set; }
    }
}