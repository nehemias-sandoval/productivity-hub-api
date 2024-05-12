using FluentValidation;
using Microsoft.EntityFrameworkCore;
using productivity_hub_api.Automappers;
using productivity_hub_api.DTOs.Proyecto;
using productivity_hub_api.Models;
using productivity_hub_api.Repository;
using productivity_hub_api.Seeders;
using productivity_hub_api.Service;
using productivity_hub_api.Validators.Proyecto;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Validator
builder.Services.AddScoped<IValidator<CreateProyectoDto>, CreateProyectoValidator>();
builder.Services.AddScoped<IValidator<UpdateProyectoDto>, UpdateProyectoValidator>();

// Mappers
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Repository
builder.Services.AddKeyedScoped<IRepository<Proyecto>, ProyectoRepository>("proyectoRepository");

// Service
builder.Services.AddKeyedScoped<ICommonService<ProyectoDto, CreateProyectoDto, UpdateProyectoDto>, ProyectoService>("proyectoService");

// Entity Framework
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    FrecuenciaSeeder.Initialize(services);
    PrioridadSeeder.Initialize(services);
    TipoNotificacionSeeder.Initialize(services);
    EstadoInvitacionSeeder.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
