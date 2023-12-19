using Libreria.Utils;

namespace Libreria.Models;

public class Producto
{
    public int IdProducto { get; set; }
    public string NombreProducto { get; set; }

    public string? DescripcionProducto { get; set; }

    public Categoria CategoriaProducto { get; set; }

    public int Stock { get; set; }
    public string? ImagenProducto { get; set; }
}
