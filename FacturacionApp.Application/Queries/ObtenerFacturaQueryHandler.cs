using FacturacionApp.Application.Dtos;
using FacturacionApp.Application.Interfaces;
using MediatR;

namespace FacturacionApp.Application.Queries
{
    public class ObtenerFacturaQueryHandler: IRequestHandler<ObtenerFacturaQuery,FacturaResponseDto>
    {
        private readonly IFacturaReadRepository _repository;
        public ObtenerFacturaQueryHandler(IFacturaReadRepository repository)
        {
                _repository = repository;
        }


        public async Task<FacturaResponseDto> Handle(ObtenerFacturaQuery query, CancellationToken cancellationToken) 
        {

            var factura = await _repository.ObtenerPorId(query.Id);

            if (factura is null)
                throw new KeyNotFoundException(nameof(factura));

            FacturaResponseDto dto = new FacturaResponseDto
            {
                ClienteNombre = factura.Cliente.Nombre,
                Id = factura.Id,
                Total = factura.Total.Cantidad,
                TotalProductos = factura.Productos.Count(),
            };

            return dto;
        }


    }
}
