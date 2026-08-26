using FacturacionApp.Domain.Interfaces;

namespace FacturacionApp.Domain.Factories
{
    public class DescuentoFactory : IDescuentoFactory
    {
        public DescuentoFactory() { }
        public IDescuento Crear(string tipo, decimal valor) => tipo switch
        {
            "fijo" => new DescuentoFijo(valor),
            "porcentaje" => new DescuentoPorcentaje(valor),
            "2x1" => new Descuento2X1(),
            _ => throw new ArgumentException("Tipo de descuento inválido")
        };
    }
}
