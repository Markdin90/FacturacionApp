using FacturacionApp.Domain.Events;
using FacturacionApp.Domain.Interfaces;

namespace FacturacionApp.Infrastructure.Handlers
{
    public class AuditoriaHandler : IDomainEventHandler
    {
        public AuditoriaHandler()
        {

        }

        public void Handle(object evento){

            if (evento is not FacturaCreada e) return;


            Console.WriteLine($"Auditoría: Factura {e.FacturaId} creada a las {e.OcurridoEn}");

            //handler manual
            //Console.WriteLine($"Auditoría: Factura {evento.Id} creada");
        }
    }
}
