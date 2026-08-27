using Castle.Components.DictionaryAdapter.Xml;
using FacturacionApp.Application.Commands;
using FacturacionApp.Application.Dtos;
using FacturacionApp.Application.Interfaces;
using FacturacionApp.Domain.Aggregates;
using FacturacionApp.Domain.Interfaces;
using Moq;

namespace Facturacion.Tests;

public class CrearFacturaCommandHandlerTests
{
    private readonly Mock<IFacturaWriteRepository> _repoMock;
    private readonly Mock<IDomainEventHandler> _eventHandlerMock;
    private readonly CrearFacturaCommandHandler _handler;

    public CrearFacturaCommandHandlerTests()
    {
        _repoMock = new Mock<IFacturaWriteRepository>();
        _eventHandlerMock = new Mock<IDomainEventHandler>();

        _handler = new CrearFacturaCommandHandler(
            _repoMock.Object,
            new List<IDomainEventHandler> { _eventHandlerMock.Object }
        );
    }

    [Fact]
    public async Task Handle_DeberiaGuardarFactura_CuandoCommandEsValido()
    {
        // Arrange
        var command = new CrearFacturaCommand
        {
            //ClienteNombre = "Juan Pérez",
            ClienteNombre = null,
            ClienteEmail = "juan@test.com",
            Productos = new List<ProductoDto>
            {
                new ProductoDto { Nombre = "Laptop", Precio = 1000, Cantidad = 1 }
            }
        };

        // Act
        var resultado = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _repoMock.Verify(r => r.Guardar(It.IsAny<Factura>()), Times.Once);
    }


    [Fact]
    public async Task Handle_DeberiaLanzarExcepcion_CuandoClienteNombreEsVacio()
    {
        // Arrange
        var command = new CrearFacturaCommand
        {
            ClienteNombre = " ",
            ClienteEmail = "juan@test.com",
            Productos = new List<ProductoDto>
        {
            new ProductoDto { Nombre = "Laptop", Precio = 1000, Cantidad = 1 }
        }
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_Que_los_eventos_se_dispararon()
    {
        // Arrange
        var command = new CrearFacturaCommand
        {
            ClienteNombre = "Marco Banda",
            ClienteEmail = "juan@test.com",
            Productos = new List<ProductoDto>
        {
            new ProductoDto { Nombre = "Laptop", Precio = 1000, Cantidad = 1 }
        }
        };

        // Act
        var resultado = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _eventHandlerMock.Verify(r => r.Handle(It.IsAny<object>()), Times.AtLeastOnce);
    }

    [Fact]
    public async Task Handle_Que_el_responseDto_tiene_los_datos_correctos()
    {
        // Arrange
        var command = new CrearFacturaCommand
        {
            ClienteNombre = "Marco Banda",
            ClienteEmail = "juan@test.com",
            Productos = new List<ProductoDto>
        {
            new ProductoDto { Nombre = "Laptop", Precio = 1000, Cantidad = 1 }
        }
        };

        // Act
        var resultado = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(resultado.ClienteNombre,command.ClienteNombre);
        Assert.Equal(1000, resultado.Total);
        Assert.Equal(1, resultado.TotalProductos);

    }
}