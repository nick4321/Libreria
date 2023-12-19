using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Libreria.Models;

namespace Libreria.Data
{
    public class ProductoContext : DbContext
    {
        public ProductoContext (DbContextOptions<ProductoContext> options)
            : base(options)
        {
        }

        public DbSet<Libreria.Models.Producto> Producto { get; set; } = default!;
    }
}
