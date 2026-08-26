using FacturacionApp.Domain.Aggregates;

namespace FacturacionApp.Application.Interfaces
{
    public interface IFacturaWriteRepository
    {
        Task Guardar(Factura factura);
        Task Eliminar(Guid id);
    }
}
