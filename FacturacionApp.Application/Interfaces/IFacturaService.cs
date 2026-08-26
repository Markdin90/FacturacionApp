using FacturacionApp.Application.Dtos;
using FacturacionApp.Domain.Aggregates;


namespace FacturacionApp.Application.Services
{
    public interface IFacturaService
    {
        Factura Crear(CrearFacturaDto dto);
    }
}
