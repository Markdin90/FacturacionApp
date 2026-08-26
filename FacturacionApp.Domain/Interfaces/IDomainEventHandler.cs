using FacturacionApp.Domain;

namespace FacturacionApp.Domain.Interfaces
{
    public interface IDomainEventHandler
    {
        //void Handle(Factura factura);
         void Handle(object evento); // recibe el evento, no la entidad
    }
}
