using FacturacionApp.Application.Interfaces;
using FacturacionApp.Domain.Aggregates;
using System.Threading.Tasks;

namespace FacturacionApp.Infrastructure.Repositories
{
    public class FacturaRepository : IFacturaWriteRepository, IFacturaReadRepository
    {
        private readonly Dictionary<Guid, Factura> _facturas;

        public FacturaRepository()
        {
            _facturas = new Dictionary<Guid, Factura>();
        }

        public async Task Guardar(Factura factura)
        {
            if (factura is null)
                throw new ArgumentNullException(nameof(factura));

            _facturas.Add(factura.Id, factura);
        }

        public async Task Eliminar(Guid id)
        {
            _facturas.Remove(id);
        }

        public async  Task<Factura?> ObtenerPorId(Guid idFactura)
        {
            _facturas.TryGetValue(idFactura, out var factura);

            return factura;
        }

        public async Task<List<Factura>> Listar() 
        {
            return _facturas.Values.ToList();
        }
    }
}
