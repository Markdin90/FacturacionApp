using FacturacionApp.Application.Commands;
using FacturacionApp.Application.Dtos;
using FacturacionApp.Application.Interfaces;
using FacturacionApp.Application.Queries;
using FacturacionApp.Application.Services;
using FacturacionApp.Domain.Aggregates;
using FacturacionApp.Domain.Factories;
using FacturacionApp.Domain.Interfaces;
using FacturacionApp.Infrastructure.Handlers;
using FacturacionApp.Infrastructure.Persistence;
using FacturacionApp.Infrastructure.Repositories;
using FacturacionApp.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CrearFacturaCommand).Assembly));

//Repository
builder.Services.AddSingleton<FacturaRepository>();
builder.Services.AddSingleton<IFacturaWriteRepository>(sp => sp.GetRequiredService<FacturaRepository>());
builder.Services.AddSingleton<IFacturaReadRepository>(sp => sp.GetRequiredService<FacturaRepository>());
builder.Services.AddScoped<IFacturaService, FacturaService>();
builder.Services.AddScoped<INotificacionService, NotificacionService>();
builder.Services.AddScoped<IFacturaWriteRepository, FacturaWriteRepository>();

//handlers
builder.Services.AddSingleton<IDomainEventHandler, EmailNotificacionHandler>();
builder.Services.AddSingleton<IDomainEventHandler, AuditoriaHandler>();
builder.Services.AddSingleton<IDomainEventHandler, ReporteHandler>();

//factory
builder.Services.AddSingleton<IDescuentoFactory, DescuentoFactory>();

//DbContext
builder.Services.AddDbContext<FacturacionDbContext>(options =>
    options.UseInMemoryDatabase("FacturacionTest"));

// Add services to the container.
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/facturas/{id}", async (Guid id, IFacturaReadRepository repo) =>
{
    try
    {
        var factura = await repo.ObtenerPorId(id);
        if (factura is null)
            return Results.NotFound();

        FacturaResponseDto dto = new FacturaResponseDto
        {
            ClienteNombre = factura.Cliente.Nombre,
            Id = id,
            Total = factura.Total.Cantidad,
            TotalProductos = factura.Productos.Count(),
        };


        return Results.Ok(dto);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapGet("/facturas/{id}/exportar", async (Guid id, IFacturaReadRepository repo) =>
{
    var factura = await repo.ObtenerPorId(id);
    if (factura is null)
        return Results.NotFound();

    if (factura is not FacturaExportable exportable)
        return Results.BadRequest("Esta factura no es exportable");

    return Results.Ok(exportable.ExportarPdf());
});

app.MapPost("/facturas", (CrearFacturaDto dto, IFacturaService service) =>
{
    try
    {
        var factura = service.Crear(dto);

        var response = new FacturaResponseDto
        {
            Id = factura.Id,
            ClienteNombre = factura.Cliente.Nombre,
            Total = factura.Total.Cantidad,
            TotalProductos = factura.Productos.Count()
        };
        return Results.Created($"/facturas/{factura.Id}", response);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapPost("/facturas/{id}/descuento", async (Guid id, AplicarDescuentoDto dto, IFacturaReadRepository repo, IDescuentoFactory factory) =>
{
    try
    {
        var factura = await repo.ObtenerPorId(id);

        if (factura is null)
            return Results.NotFound();
        
        var descuento = factory.Crear(dto.Tipo.ToString(),dto.Valor);

        factura.AplicaDescuento(descuento);
        return Results.Ok();
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});


//CQRS
// Query
app.MapGet("/facturas", async (IMediator mediator) =>
{
    var result = await mediator.Send(new ListarFacturasQuery());
    return Results.Ok(result);
});

app.MapGet("/facturascqrs/{id}", async (Guid id, IMediator mediator) =>
{
    var result = await mediator.Send(new ObtenerFacturaQuery { Id = id });
    return Results.Ok(result);
});

// Commands
app.MapPost("/facturas/crear", async (CrearFacturaCommand command, IMediator mediator) =>
{
    var result = await mediator.Send(command);
    return Results.Created($"/facturas/{result.Id}", result);
});

app.MapPost("/facturascqrs/{id}/descuento", async (Guid id, AplicarDescuentoCommand command, IMediator mediator) =>
{
    command = command with { FacturaId = id };
    await mediator.Send(command);
    return Results.Ok();
});

app.Run();

//WebAplicationFactory
public partial class Program { }

//Comentario para que el proyecto compile y se pueda testear con WebApplicationFactory, no tiene otra funcionalidad.



