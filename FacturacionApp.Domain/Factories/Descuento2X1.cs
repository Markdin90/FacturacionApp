using FacturacionApp.Domain.Entities;
using FacturacionApp.Domain.Interfaces;

namespace FacturacionApp.Domain.Factories
{
    public class Descuento2X1 : IDescuento
    {
        public Descuento2X1()
        {
            
        }

        public decimal Aplicar(decimal total, List<Producto> productos) 
        {            
            decimal subtotal = 0;

            foreach (var producto in productos) {

    
                 subtotal = subtotal + (((producto.Cantidad / 2 ) +(producto.Cantidad % 2)) * producto.Precio.Cantidad);
                
            }

            return subtotal;
        }
    }
}
