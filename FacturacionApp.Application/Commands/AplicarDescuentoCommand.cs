using MediatR;

namespace FacturacionApp.Application.Commands
{
    public record AplicarDescuentoCommand : IRequest<Unit>
    {
        public Guid FacturaId { get; set; }
        public string Tipo { get; set; }
        public decimal Valor { get; set; }
    }
}
