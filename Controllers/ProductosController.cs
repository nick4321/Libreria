using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Libreria.Data;
using Libreria.Models;

namespace Libreria.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ProductoContext _context;

        public ProductosController(ProductoContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index(string buscar, string filtro)
        {
            var productos = from producto in _context.Producto select producto;

            if (!String.IsNullOrEmpty(buscar))
            {
                productos = productos.Where(s => s.NombreProducto != null && s.NombreProducto.ToUpper().Contains(buscar.ToUpper()));
            }

            ViewData["FiltroNombreProducto"] = String.IsNullOrEmpty(filtro) ? "NombreDescendente" : "";
            ViewData["FiltroDescripcion"] = String.IsNullOrEmpty(filtro) ? "DescripcionDescendente" : "";
            ViewData["FiltroCategoriaProducto"] = String.IsNullOrEmpty(filtro) ? "CategoriaDescendente" : "";
            ViewData["FiltroStock"] = filtro == "NumeroDescendente" ? "NumeroAscendente" : "NumeroDescendente";

            switch (filtro)
            {
                case "NombreDescendente":
                    productos = productos.OrderByDescending(producto => producto.NombreProducto);
                    break;
                case "DescripcionDescendente":
                    productos = productos.OrderByDescending(productos => productos.DescripcionProducto);
                    break;
                case "CategoriaDescendente":
                    productos = productos.OrderByDescending(producto => producto.CategoriaProducto);
                    break;
                case "NumeroAscendente":
                    productos = productos.OrderByDescending(productos => productos.Stock);
                    break;
                case "NumeroDescendente":
                    productos = productos.OrderBy(productos => productos.Stock);
                    break;
                default:
                    productos = productos.OrderBy(producto => producto.NombreProducto);
                    break;
            }

            return View(await productos.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreProducto,DescripcionProducto,CategoriaProducto,Stock,ImagenProducto")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                producto.ImagenProducto = "../imagenes/" + producto.ImagenProducto;
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreProducto,DescripcionProducto,CategoriaProducto,Stock,ImagenProducto")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    producto.ImagenProducto = "../imagenes/" + producto.ImagenProducto;
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Producto.FindAsync(id);
            if (producto != null)
            {
                _context.Producto.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Producto.Any(e => e.Id == id);
        }
    }
}
