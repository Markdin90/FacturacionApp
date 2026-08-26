using FacturacionApp.Domain.Interfaces;

namespace FacturacionApp.Domain.Factories
{
    public interface IDescuentoFactory
    {
        IDescuento Crear(string tipo, decimal valor);
    }
}
