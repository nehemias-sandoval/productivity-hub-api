using FluentValidation;
using Microsoft.EntityFrameworkCore;
using productivity_hub_api.Automappers;
using productivity_hub_api.DTOs.Proyecto;
using productivity_hub_api.DTOs.Subtarea;
using productivity_hub_api.DTOs.Tarea;
using productivity_hub_api.Models;
using productivity_hub_api.Repository;
using productivity_hub_api.Seeders;
using productivity_hub_api.Service;
using productivity_hub_api.Validators.Proyecto;
using productivity_hub_api.Validators.Subtarea;
using productivity_hub_api.Validators.Tarea;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Validator
builder.Services.AddScoped<IValidator<CreateProyectoDto>, CreateProyectoValidator>();
builder.Services.AddScoped<IValidator<UpdateProyectoDto>, UpdateProyectoValidator>();

builder.Services.AddScoped<IValidator<CreateTareaDto>, CreateTareaValidator>();
builder.Services.AddScoped<IValidator<UpdateTareaDto>, UpdateTareaValidator>();

builder.Services.AddScoped<IValidator<CreateSubtareaDto>, CreateSubtareaValidator>();
builder.Services.AddScoped<IValidator<UpdateSubtareaDto>,  UpdateSubtareaValidator>();

// Mappers
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Repository
builder.Services.AddKeyedScoped<IRepository<Proyecto>, ProyectoRepository>("proyectoRepository");
builder.Services.AddKeyedScoped<IRepository<Tarea>, TareaRepository>("tareaRepository");
builder.Services.AddKeyedScoped<IRepository<Subtarea>, SubtareaRepository>("subtareaRepository");
// Service
builder.Services.AddKeyedScoped<ICommonService<ProyectoDto, CreateProyectoDto, UpdateProyectoDto>, ProyectoService>("proyectoService");
builder.Services.AddKeyedScoped<ICommonService<TareaDto, CreateTareaDto, UpdateTareaDto>, TareaService>("tareaService");
builder.Services.AddKeyedScoped<ICommonService<SubtareaDto, CreateSubtareaDto, UpdateSubtareaDto>, SubtareaService>("subtareaService");

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
