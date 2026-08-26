using FacturacionApp.Application.Interfaces;
using FacturacionApp.Domain.Factories;
using MediatR;

namespace FacturacionApp.Application.Commands
{
    public class AplicarDescuentoCommandHandler : IRequestHandler<AplicarDescuentoCommand, Unit>
    {
        private readonly IFacturaReadRepository _repo;
        private readonly IDescuentoFactory _factory;
        public AplicarDescuentoCommandHandler(IFacturaReadRepository repo, IDescuentoFactory factory)
        {
            _repo = repo;
            _factory = factory;
        }

        public async Task<Unit> Handle(AplicarDescuentoCommand command, CancellationToken cancellationToken) 
        {
            var factura = await _repo.ObtenerPorId(command.FacturaId);

            if (factura is null)
                throw new KeyNotFoundException("no se encontró la factura");

            var descuento = _factory.Crear(command.Tipo.ToString(), command.Valor);

            factura.AplicaDescuento(descuento);

            return Unit.Value;
        }
    }
}
