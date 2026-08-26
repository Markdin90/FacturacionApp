namespace FacturacionApp.Application.Dtos
{
    public class AplicarDescuentoDto
    {
        public string Tipo { get; set; }  // "fijo", "porcentaje", "2x1"
        public decimal Valor { get; set; } // solo aplica para fijo y porcentaje
    }
}
