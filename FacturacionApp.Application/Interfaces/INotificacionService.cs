using FacturacionApp.Domain.Aggregates;

namespace FacturacionApp.Services
{
    public interface INotificacionService
    {
        void Enviar(Factura factura);
    }
}
