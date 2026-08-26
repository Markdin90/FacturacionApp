using FacturacionApp.Domain.Aggregates;

namespace FacturacionApp.Services
{
    public class NotificacionService : INotificacionService
    {
        public NotificacionService()
        {
                
        }

        public void Enviar(Factura factura)
        {
            // simula envío, solo un Console.WriteLine por ahora
            Console.WriteLine($"Email enviado a {factura.Cliente.Email} - Factura {factura.Id}");
        }
    }
}
