using VideoClub.Models;

namespace VideoClub.ViewModels;

public class ArticuloElencoViewModel
{
    public List<ElencoArticulo> ElencoArticulo { get; set; } = new();
    public List<Elenco> Elenco { get; set; } = new();
    public List<Articulos> Articulos { get; set; } = new();
    public List<TiposArticulos> TiposArticulos { get; set; } = new();
    public List<Idiomas> Idiomas { get; set; } = new();
}
