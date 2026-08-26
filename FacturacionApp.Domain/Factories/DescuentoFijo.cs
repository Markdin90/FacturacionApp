using FacturacionApp.Domain.Entities;
using FacturacionApp.Domain.Interfaces;

namespace FacturacionApp.Domain.Factories
{
    public class DescuentoFijo : IDescuento
    {
        private readonly decimal _monto;
        public DescuentoFijo(decimal monto)
        {
            _monto = monto;
        }

        public decimal Aplicar(decimal total, List<Producto> productos) {            

            return total -= _monto;
        }
    }
}
