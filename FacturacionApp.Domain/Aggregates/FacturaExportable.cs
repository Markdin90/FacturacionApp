using FacturacionApp.Domain.Entities;

namespace FacturacionApp.Domain.Aggregates
{
    public class FacturaExportable : Factura
    {
        public FacturaExportable(ClienteFacturacion cliente): base(cliente)
        {
            
        }

        public string ExportarPdf() 
        {
            // simula exportación
            return $"PDF-{Id}-{Cliente.Nombre}-{Total}";
        }
    }
}
