using FacturacionApp.Domain.Entities;

namespace FacturacionApp.Domain.Interfaces
{
    // Patrón: Strategy
    // Permite intercambiar algoritmos de descuento sin modificar Factura
    public interface IDescuento
    {
        decimal Aplicar(decimal total, List<Producto> productos);
    }
}
