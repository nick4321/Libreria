using Libreria.Models;

namespace Libreria.Services;

public interface IProductoService{

    void Create(Producto obj);
    void Update(Producto obj);
    void Delete(Producto obj);
    Producto? GetById(int id);
}