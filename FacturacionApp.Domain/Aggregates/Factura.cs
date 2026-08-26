using FacturacionApp.Domain.Entities;
using FacturacionApp.Domain.Events;
using FacturacionApp.Domain.Interfaces;
using FacturacionApp.Domain.ValueObjects;

namespace FacturacionApp.Domain.Aggregates
{
    public class Factura
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public ClienteFacturacion Cliente { get; private set; }

        private readonly List<Producto> _productos = new ();
        public IReadOnlyList<Producto> Productos => _productos;

        public Dinero Total { get; private set; } = new Dinero(0);
        public bool EstaConfirmada { get; private set; }


        private readonly List<object> _eventos = new();
        public IReadOnlyList<object> Eventos => _eventos;

        public Factura(ClienteFacturacion cliente) 
        {            

            if (cliente is null)
                throw new ArgumentException("No se puede generar factura sin cliente");
            
            Cliente = cliente;            
        }

        //Para pruebas de integracion
        private Factura() { }

        public void AplicaDescuento(IDescuento descuento)
        {             
            Total = new Dinero(descuento.Aplicar(Total.Cantidad,_productos));
        }

        public void AgregarProducto(Producto producto)
        {
            if (producto is null)
                throw new ArgumentException("Producto inválido");

            if (EstaConfirmada)
                throw new InvalidOperationException("No puedes modificar una factura confirmada");

            _productos.Add(producto);

            Total = new Dinero(_productos.Sum(p => p.Precio.Cantidad * p.Cantidad)); // recalcula automático
        }

        public void Confirmar()
        {
            if (!_productos.Any())
                throw new ArgumentException("Debe tener al menos un producto");

            EstaConfirmada = true;

            _eventos.Add(new FacturaCreada(Id, Cliente.Email.Valor, Total.Cantidad));
        }
    }
}
