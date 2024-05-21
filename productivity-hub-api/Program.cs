using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using productivity_hub_api.Automappers;
using productivity_hub_api.DTOs.Auth;
using productivity_hub_api.DTOs.Proyecto;
using productivity_hub_api.helpers;
using productivity_hub_api.Models;
using productivity_hub_api.Repository;
using productivity_hub_api.Seeders;
using productivity_hub_api.Service;
using productivity_hub_api.Validators.Auth;
using productivity_hub_api.Validators.Proyecto;

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

// Mappers
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Repository
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddKeyedScoped<IRepository<Proyecto>, ProyectoRepository>("proyectoRepository");

// Configuration
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Service
builder.Services.AddScoped<IUsuarioService<
    UsuarioDto, CreateUsuarioDto, UpdateUsuarioDto, AuthenticateReqDto, AuthenticateResDto>, UsuarioService>();
builder.Services.AddKeyedScoped<ICommonService<ProyectoDto, CreateProyectoDto, UpdateProyectoDto>, ProyectoService>("proyectoService");

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
