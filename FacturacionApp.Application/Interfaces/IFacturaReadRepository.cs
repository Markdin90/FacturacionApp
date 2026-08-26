using FacturacionApp.Domain.Aggregates;

namespace FacturacionApp.Application.Interfaces
{
    public interface IFacturaReadRepository
    {
        Task<Factura?> ObtenerPorId(Guid id);
        Task<List<Factura>> Listar();
    }
}
