using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoClub.Models
{
    [Table("Articulos")]
    public class Articulos
    {

        public enum EstadoArticulo
        {
            [Display(Name = "Retrasado")]
            Retrasado = 3,
            [Display(Name = "Rentado")]
            Rentado = 2,
            [Display(Name = "Disponible")]
            Disponible = 1,
            [Display(Name = "Inactivo")]
            Inactivo = 0,
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string? Titulo { get; set; }

        [Column("Tipos_de_ArticulosId")]
        [ForeignKey("TiposArticulos")]
        public int TiposArticulosId { get; set; }
        public TiposArticulos? TiposArticulos { get; set; }

        [ForeignKey("Idioma")]
        public int IdiomaId { get; set; }
        public Idiomas? Idioma { get; set; }

        [Column("Renta_por_dia")]
        public double RentaPorDia { get; set; }
        
        [Column("Dias_de_renta")]
        public int DiasDeRenta { get; set; }

        [Column("Monto_Entrega_Tardia")]
        public double MontoEntregaTardia {get; set;}

        public EstadoArticulo Estado { get; set; }
    }
}