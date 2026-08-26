namespace FacturacionApp.Domain.ValueObjects
{
    public class Dinero
    {
        public decimal Cantidad { get;}

        public Dinero(decimal cantidad) 
        {
            if (cantidad < 0)
                throw new ArgumentException("el valor no puede ser negativo");

            Cantidad = cantidad;
        }

        //Para pruebas de integracion
        private Dinero()
        {
            
        }
    }
}
