using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoClub.Models
{
    [Table("Clientes")]
    public class Clientes
    {

        public enum EstadoCliente
        {
            Activo = 1,
            Inactivo = 0
        }

        public enum TipoPersona
        {
            Fisica = 1,
            Juridica = 2
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Nombre { get; set; }

        public string Cedula { get; set; }

        public string NumeroTarjetaCR { get; set; }

        public double Limite_Credito { get; set; }

        public TipoPersona Tipo_Persona { get; set; }

        public double Monto_Entrega_Tardia {get; set;}

        public EstadoCliente Estado { get; set; }
    }
}