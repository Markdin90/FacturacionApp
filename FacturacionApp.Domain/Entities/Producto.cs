using FacturacionApp.Domain.ValueObjects;

namespace FacturacionApp.Domain.Entities
{
    public class Producto
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Nombre { get; private set; }
        public Dinero Precio { get; private set; }
        public int Cantidad { get; private set; }

        public Producto(string nombre,Dinero precio, int cantidad)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("Nombre es necesario");            

            if (cantidad <= 0)
                throw new ArgumentException("Cantidad debe ser mayor a 0");

            Nombre = nombre;
            Precio = precio;
            Cantidad = cantidad;
        }

        //Para pruebas de integracion
        private Producto()
        {
            
        }
    }
}
