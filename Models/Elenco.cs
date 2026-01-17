using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoClub.Models
{
    [Table("Elenco")]
    public class Elenco
    {

        public enum EstadoElenco
        {
            Activo = 1,
            Inactivo = 0
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        public EstadoElenco Estado { get; set; }
    }
}