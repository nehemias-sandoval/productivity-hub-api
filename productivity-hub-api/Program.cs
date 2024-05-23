using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using productivity_hub_api.Automappers;
using productivity_hub_api.DTOs.Auth;
using productivity_hub_api.DTOs.Proyecto;
using productivity_hub_api.helpers;
using productivity_hub_api.DTOs.Subtarea;
using productivity_hub_api.DTOs.Tarea;
using productivity_hub_api.Models;
using productivity_hub_api.Repository;
using productivity_hub_api.Seeders;
using productivity_hub_api.Service;
using productivity_hub_api.Validators.Auth;
using productivity_hub_api.Validators.Proyecto;
using productivity_hub_api.Validators.Subtarea;
using productivity_hub_api.Validators.Tarea;
using productivity_hub_api.DTOs.Evento;
using productivity_hub_api.Validators.Evento;
using productivity_hub_api.DTOs.TareaEtiqueta;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(swagger =>
{
    // This is to generate the Default UI of Swagger Documentation
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Productiviry Hub API",
        Description = ".NET Core 8 Web API"
    });

    // To Enable authorization using Swagger (JWT)
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });

    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddHttpContextAccessor();

// Validator
builder.Services.AddScoped<IValidator<AuthenticateReqDto>, LoginValidator>();
builder.Services.AddScoped<IValidator<CreateUsuarioDto>, CreateUsuarioValidator>();
builder.Services.AddScoped<IValidator<UpdateUsuarioDto>, UpdateUsuarioValidator>();

builder.Services.AddScoped<IValidator<CreateProyectoDto>, CreateProyectoValidator>();
builder.Services.AddScoped<IValidator<UpdateProyectoDto>, UpdateProyectoValidator>();

builder.Services.AddScoped<IValidator<CreateEventoDto>, CreateEventoValidator>();
builder.Services.AddScoped<IValidator<UpdateEventoDto>, UpdateEventoValidator>();

builder.Services.AddScoped<IValidator<CreateTareaDto>, CreateTareaValidator>();
builder.Services.AddScoped<IValidator<UpdateTareaDto>, UpdateTareaValidator>();

builder.Services.AddScoped<IValidator<CreateSubtareaDto>, CreateSubtareaValidator>();
builder.Services.AddScoped<IValidator<UpdateSubtareaDto>,  UpdateSubtareaValidator>();

// Mappers
builder.Services.AddAutoMapper(typeof(MappingProfile));

// UnitOfWork 
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Repository
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddKeyedScoped<IRepository<Proyecto>, ProyectoRepository>("proyectoRepository");
builder.Services.AddKeyedScoped<IRepository<Evento>, EventoRepository>("eventoRepository");
builder.Services.AddKeyedScoped<IRepository<Tarea>, TareaRepository>("tareaRepository");
builder.Services.AddKeyedScoped<IRepository<Subtarea>, SubtareaRepository>("subtareaRepository");
builder.Services.AddScoped<ProyectoTareaRepository>();
builder.Services.AddScoped<EventoTareaRepository>();
builder.Services.AddScoped<CatalogoRepository>();

// Configuration
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Service
builder.Services.AddScoped<IUsuarioService<
    UsuarioDto, CreateUsuarioDto, UpdateUsuarioDto, AuthenticateReqDto, AuthenticateResDto>, UsuarioService>();
builder.Services.AddKeyedScoped<ICommonService<ProyectoDto, CreateProyectoDto, UpdateProyectoDto>, ProyectoService>("proyectoService");
builder.Services.AddKeyedScoped<ICommonService<EventoDto, CreateEventoDto, UpdateEventoDto>, EventoService>("eventoService");
builder.Services.AddKeyedScoped<ITareaService<TareaDto, CreateTareaDto, UpdateTareaDto, CreateTareaEtiquetaDto>, TareaService>("tareaService");
builder.Services.AddKeyedScoped<ISubtareaService<SubtareaDto, CreateSubtareaDto, UpdateSubtareaDto>, SubtareaService>("subtareaService");

// Entity Framework
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    FrecuenciaSeeder.Initialize(services);
    PrioridadSeeder.Initialize(services);
    TipoNotificacionSeeder.Initialize(services);
    TipoEventoSeeder.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
