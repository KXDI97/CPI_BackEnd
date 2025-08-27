using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using CPI_Backend.API.Data;
using CPI_Backend.API.Endpoints;
using CPI_Backend.API.Models.DTO;


//using CPI_Backend.API.Models.Para_organizar;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Agrega el servicio CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }
    );
});

// OpenAPI (Swagger)
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CPI API", Version = "v1" });
//});

builder.Configuration.AddJsonFile(
    "appsettings.Development.json",
    optional: false,
    reloadOnChange: true
);

//Selección de manejador de base de datos
builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
    {
        //Conecta con Postgres en Mac
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    //Conecta con MySQLServer en Windows
    {
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        }

    }
    else
    {
        throw new Exception("Sistema operativo no soportado");
    }
});

var app = builder.Build();

// Usa CORS antes del mapeo de rutas
app.UseCors("AllowAll");

//Swagger solo en desarrollo
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

//Invoca el Endpoint de Usuarios
app.MapUsersEndpoints();

//Invoca el Endpoint del módulo Storage
app.MapProductEndpoints();


//Invoca el Endpoint de Compras - Activar cuando todo este bien
//app.MapPurchaseEndpoints();

app.UseHttpsRedirection();

app.Run();
