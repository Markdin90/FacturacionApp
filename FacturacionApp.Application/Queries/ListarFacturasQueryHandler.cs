using FacturacionApp.Application.Dtos;
using FacturacionApp.Application.Interfaces;
using MediatR;

namespace FacturacionApp.Application.Queries
{
    public class ListarFacturasQueryHandler : IRequestHandler<ListarFacturasQuery,List<FacturaResponseDto>>
    {
        private readonly IFacturaReadRepository _repository;
        public ListarFacturasQueryHandler(IFacturaReadRepository repository)
        {
             _repository = repository;
        }

        public async Task<List<FacturaResponseDto>> Handle(ListarFacturasQuery query, CancellationToken cancellationToken)
        {            
            var facturas = await _repository.Listar();

            return facturas.Select(f => new FacturaResponseDto 
            {
                ClienteNombre = f.Cliente.Nombre,
                Id = f.Id,
                Total = f.Total.Cantidad,
                TotalProductos = f.Productos.Count()
            }).ToList();

        }
    }
}
