using FacturacionApp.Application.Interfaces;
using FacturacionApp.Domain.Aggregates;
using FacturacionApp.Infrastructure.Persistence;

namespace FacturacionApp.Infrastructure.Repositories
{
    public class FacturaWriteRepository : IFacturaWriteRepository
    {
        private readonly FacturacionDbContext _context;

        public FacturaWriteRepository(FacturacionDbContext context)
        {
            _context = context;
        }

        public async Task Eliminar(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task Guardar(Factura factura)
        {
            await _context.Facturas.AddAsync(factura);
            await _context.SaveChangesAsync();
        }
    }
}
