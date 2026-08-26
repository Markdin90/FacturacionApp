using FacturacionApp.Application.Dtos;
using MediatR;

namespace FacturacionApp.Application.Queries
{
    public record ListarFacturasQuery: IRequest<List<FacturaResponseDto>>
    {
    }
}
