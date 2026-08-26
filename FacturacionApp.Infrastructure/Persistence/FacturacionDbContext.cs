using FacturacionApp.Domain.Aggregates;
using FacturacionApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FacturacionApp.Infrastructure.Persistence
{
    public class FacturacionDbContext: DbContext
    {
        public FacturacionDbContext(DbContextOptions<FacturacionDbContext> options): base(options)
        {
            
        }

        public DbSet<Factura> Facturas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Factura>(builder =>
            {
                builder.OwnsOne(f => f.Cliente, cliente =>
                {
                    cliente.OwnsOne(c => c.Email);
                });
                builder.OwnsMany(f => f.Productos, productos =>
                {
                    productos.OwnsOne(p => p.Precio);
                });
                builder.Ignore(f => f.Total);
            });            
        }
    }
}
