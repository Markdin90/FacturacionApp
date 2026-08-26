using FacturacionApp.Domain.Entities;
using FacturacionApp.Domain.Interfaces;

namespace FacturacionApp.Domain.Factories
{
    public class DescuentoPorcentaje : IDescuento
    {
        private readonly decimal _porcentaje;
        public DescuentoPorcentaje(decimal porcentaje)
        {
            _porcentaje = porcentaje;
        }


        public decimal Aplicar(decimal total, List<Producto> productos) 
        {            

            return total -= total * (_porcentaje / 100);
        }
    }
}
