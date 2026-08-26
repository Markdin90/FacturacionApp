namespace FacturacionApp.Application.Dtos
{
    public class CrearFacturaDto
    {
        public string ClienteNombre { get; set; }
        public string ClienteEmail { get; set; }
        public List<ProductoDto> Productos { get; set; }
    }
}
