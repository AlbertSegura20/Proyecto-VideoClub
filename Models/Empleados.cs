using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoClub.Models
{
    [Table("Empleados")]
    public class Empleados
    {

        public enum EstadoEmpleados
        {
            Activo = 1,
            Inactivo = 0
        }


        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Nombre { get; set; }

        public string Correo { get; set; }

        public string Password { get; set; }

        public string Cedula { get; set; }

        public string TandaLabor { get; set; }

        public double PorcentajeComision { get; set; }

        public DateTime Fecha_Ingreso { get; set; }

        public EstadoEmpleados Estado { get; set; }
    }
}