using FacturacionApp.Application.Dtos;
using MediatR;

namespace FacturacionApp.Application.Queries
{
    public record ObtenerFacturaQuery : IRequest<FacturaResponseDto>
    {
        public Guid Id { get; set; }
    }
}
