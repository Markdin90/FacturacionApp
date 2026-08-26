using FacturacionApp.Application.Dtos;
using FacturacionApp.Application.Interfaces;
using FacturacionApp.Domain.Aggregates;
using FacturacionApp.Domain.Entities;
using FacturacionApp.Domain.Interfaces;
using FacturacionApp.Domain.ValueObjects;
using MediatR;

namespace FacturacionApp.Application.Commands
{
    public class CrearFacturaCommandHandler: IRequestHandler<CrearFacturaCommand, FacturaResponseDto>
    {
        private readonly IFacturaWriteRepository _repo;
        private readonly IEnumerable<IDomainEventHandler> _handlers;

        public CrearFacturaCommandHandler(IFacturaWriteRepository repo, IEnumerable<IDomainEventHandler> handlers)
        {
            _repo = repo;   
            _handlers = handlers;
        }

        public async Task<FacturaResponseDto> Handle(CrearFacturaCommand request, CancellationToken cancellationToken)
        {
            ClienteFacturacion cliente = new ClienteFacturacion(request.ClienteNombre, request.ClienteEmail);

            var factura = new Factura(cliente);

            foreach (var p in request.Productos)
            {
                var producto = new Producto(p.Nombre, new Dinero(p.Precio), p.Cantidad);

                factura.AgregarProducto(producto);
            }

            factura.Confirmar();
            await _repo.Guardar(factura);

            //dispara handlers manualmente
            //foreach (var handler in _handlers)
            //    handler.Handle(factura); // no sabe quién es cada uno

            //implementa eventos creados desde factura
            foreach (var evento in factura.Eventos)
                foreach (var handler in _handlers)
                    handler.Handle(evento);

            FacturaResponseDto responseDto = new FacturaResponseDto
            {
                ClienteNombre = cliente.Nombre,
                Id = factura.Id,
                Total = factura.Total.Cantidad,
                TotalProductos = factura.Productos.Count(),
            };

            return responseDto;

        }
    }
}
