using FacturacionApp.Domain.Events;
using FacturacionApp.Domain.Interfaces;

namespace FacturacionApp.Infrastructure.Handlers
{
    public class ReporteHandler : IDomainEventHandler
    {
        public ReporteHandler() { }

        public void Handle(object evento) 
        {
            if (evento is not FacturaCreada e) return; // ignora lo que no le importa

            Console.WriteLine($"Reporte: Total del día actualizado +{e.Total}");

            //handler manual
            //Console.WriteLine($"Reporte: Total del día actualizado +{factura.Total}");
        }
    }
}
