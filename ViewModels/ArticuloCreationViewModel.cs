using System.ComponentModel.DataAnnotations;
using VideoClub.Models;

namespace VideoClub.ViewModels
{
    public class ArticuloCreationViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; } = null!;
        public int TiposArticulosId { get; set; }
        public int IdiomaId { get; set; }
        public double RentaPorDia { get; set; }
        public double MontoEntregaTardia { get; set; }
        public int Estado { get; set; }

        public List<ElencoCreationDto> Elenco { get; set; } = new List<ElencoCreationDto>();
    }

    public class ElencoCreationDto
    {
        public int ElencoId { get; set; }
        public string Rol { get; set; } = null!;
    }
}
