namespace FacturacionApp.Domain.Events
{
    public class FacturaCreada
    {
        public Guid FacturaId { get; }
        public string ClienteEmail { get; }
        public decimal Total { get; }
        public DateTime OcurridoEn { get; }

        public FacturaCreada(Guid facturaId, string clienteEmail, decimal total)
        {
            FacturaId = facturaId;
            ClienteEmail = clienteEmail;
            Total = total;
            OcurridoEn = DateTime.UtcNow;
        }
    }
}
