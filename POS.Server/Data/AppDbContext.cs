using Microsoft.EntityFrameworkCore;
using POS.Shared.Entities;

namespace POS.Server.Data
{
        public class AppDbContext : DbContext
        {
            // Este constructor es OBLIGATORIO para que funcione AddDbContext
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

            // Aquí defines las tablas de tu base de datos
            public DbSet<Product> Products { get; set; }
            public DbSet<Category> Categories { get; set; }

            public DbSet<User> Users { get; set; }
    }
    }
