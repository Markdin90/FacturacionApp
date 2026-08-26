using FacturacionApp.Application.Dtos;
using FacturacionApp.Application.Interfaces;
using FacturacionApp.Domain.Aggregates;
using FacturacionApp.Domain.Entities;
using FacturacionApp.Domain.Interfaces;
using FacturacionApp.Domain.ValueObjects;

namespace FacturacionApp.Application.Services
{
    public class FacturaService : IFacturaService
    {
        private readonly IFacturaWriteRepository _repo;
        private readonly IEnumerable<IDomainEventHandler> _handlers;

        public FacturaService(IFacturaWriteRepository  repo, IEnumerable<IDomainEventHandler> handlers) {
        
            _repo = repo;            
            _handlers = handlers;
        }

        public Factura Crear(CrearFacturaDto dto) 
        {            
            ClienteFacturacion cliente = new ClienteFacturacion(dto.ClienteNombre, dto.ClienteEmail);

            var factura = new Factura(cliente);

            foreach (var p in dto.Productos)
            {
                var producto = new Producto(p.Nombre, new Dinero(p.Precio), p.Cantidad);

                factura.AgregarProducto(producto);
            }

            factura.Confirmar();
            _repo.Guardar(factura);            

            //dispara handlers manualmente
            //foreach (var handler in _handlers)
            //    handler.Handle(factura); // no sabe quién es cada uno

            //implementa eventos creados desde factura
            foreach (var evento in factura.Eventos)
                foreach (var handler in _handlers)
                    handler.Handle(evento);

            return factura;

        }
    }
}
