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
            public DbSet<Client> Clients { get; set; }
            public DbSet<Venta> Ventas { get; set; }
            public DbSet<DetalleVenta> DetallesVenta { get; set; }
            public DbSet<UnidadFisica> UnidadesFisicas { get; set; }
            public DbSet<Marca> Marcas { get; set; }
            public DbSet<Proveedor> Proveedores { get; set; }
            public DbSet<LoteImportacion> LotesImportacion { get; set; }
            public DbSet<Garantia> Garantias { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                // Configuración de la relación uno a uno entre UnidadFisica y Garantia
                // Le indicamos a EF Core que la tabla Garantia es la que contiene la llave foránea (IdUnidad)
                modelBuilder.Entity<UnidadFisica>()
                    .HasOne(u => u.Garantia)
                    .WithOne(g => g.UnidadFisica)
                    .HasForeignKey<Garantia>(g => g.IdUnidad);

            modelBuilder.Entity<DetalleVenta>()
                    .HasOne(d => d.Producto)
                    .WithMany() // Si tu entidad Product no tiene una lista de Detalles, déjalo vacío
                    .HasForeignKey(d => d.IdProducto);
        }
        }
    }
