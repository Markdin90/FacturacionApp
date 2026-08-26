using FacturacionApp.Application.Dtos;
using MediatR;

namespace FacturacionApp.Application.Commands
{
    public record CrearFacturaCommand : IRequest<FacturaResponseDto>
    {
        public string ClienteNombre { get; set; }
        public string ClienteEmail { get; set; }
        public List<ProductoDto> Productos { get; set; }


    }
}
