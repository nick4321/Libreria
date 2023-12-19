using Libreria.Data;
using Libreria.Models;
using Microsoft.EntityFrameworkCore;

namespace Libreria.Services;

public class ProductoService : IProductoService
{
    private readonly ProductoContext _context;

    public ProductoService(ProductoContext context)
    {
        _context = context;
    }

    public void Create(Producto obj)
    {
        _context.Add(obj);
        _context.SaveChanges();
    }

    public void Delete(Producto obj)
    {
        _context.Remove(obj);
        _context.SaveChanges();
    }

     public Producto? GetById(int id)
    {
        var producto = _context.Producto
                .FirstOrDefault(m => m.Id == id);

        return producto;
    }


    public void Update(Producto obj)
    {
        _context.Update(obj);
        _context.SaveChanges();
    }
}