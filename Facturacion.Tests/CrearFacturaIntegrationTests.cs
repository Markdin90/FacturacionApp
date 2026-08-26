using FacturacionApp.Application.Commands;
using FacturacionApp.Application.Dtos;
using FacturacionApp.Application.Interfaces;
using FacturacionApp.Domain.Interfaces;
using FacturacionApp.Infrastructure.Persistence;
using FacturacionApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Facturacion.Tests
{
    public class CrearFacturaIntegrationTests
    {
        private readonly FacturacionDbContext _context;
        private readonly IFacturaWriteRepository _repo;
        private readonly Mock<IDomainEventHandler> _eventHandlerMock;
        private readonly CrearFacturaCommandHandler _handler;

        public CrearFacturaIntegrationTests()
        {
            var options = new DbContextOptionsBuilder<FacturacionDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new FacturacionDbContext(options);
            _repo = new FacturaWriteRepository(_context);
            _eventHandlerMock = new Mock<IDomainEventHandler>();

            _handler = new CrearFacturaCommandHandler(
                _repo,
                new List<IDomainEventHandler> { _eventHandlerMock.Object }
            );
        }

        [Fact]
        public async Task Handle_DeberiaGuardarFacturaEnBaseDeDatos_CuandoCommandEsValido()
        {
            // Arrange
            var command = new CrearFacturaCommand
            {
                ClienteNombre = "Marco Banda",
                ClienteEmail = "marco@test.com",
                Productos = new List<ProductoDto>
            {
                new ProductoDto { Nombre = "Laptop", Precio = 1000, Cantidad = 1 }
            }
            };

            // Act
            var resultado = await _handler.Handle(command, CancellationToken.None);

            // Assert
            var facturaEnDb = await _context.Facturas.FindAsync(resultado.Id);
            Assert.NotNull(facturaEnDb);
        }
    }
}
