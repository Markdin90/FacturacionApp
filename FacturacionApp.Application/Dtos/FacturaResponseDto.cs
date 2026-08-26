namespace FacturacionApp.Application.Dtos
{
    public class FacturaResponseDto
    {
        public Guid Id { get; set; }
        public string ClienteNombre { get; set; }
        public decimal Total { get; set; }
        public int TotalProductos { get; set; }
    }
}
