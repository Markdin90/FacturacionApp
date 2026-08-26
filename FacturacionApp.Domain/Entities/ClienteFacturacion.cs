using FacturacionApp.Domain.ValueObjects;

namespace FacturacionApp.Domain.Entities
{
    public class ClienteFacturacion
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Nombre { get; private set; }
        public Email Email { get; private set; }

        public ClienteFacturacion(string nombre, string email)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentNullException("Nombre no debe estar vacio.");                      

            Nombre = nombre;
            Email = new Email(email);
        }

        //Para pruebas de integracion
        private ClienteFacturacion()
        {
            
        }
    }
}
