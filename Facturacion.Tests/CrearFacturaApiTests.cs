using FacturacionApp.Application.Commands;
using FacturacionApp.Application.Dtos;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http.Json;

namespace Facturacion.Tests
{
    public class CrearFacturaApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CrearFacturaApiTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task POST_CrearFactura_DeberiaRetornar201_CuandoCommandEsValido()
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
            var response = await _client.PostAsJsonAsync("/facturas/crear", command);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
